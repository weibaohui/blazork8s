using System;
using BlazorApp.GptWorkflow.Actions;
using BlazorApp.GptWorkflow.Steps;
using BlazorApp.GptWorkflow.Tools;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Workflow;

public class DoWhileWorkflow : IGptWorkflow<GlobalContext>
{
    public static string Name => "DoWhileWorkflow";
    public string Id => "DoWhileWorkflow";
    public int Version => 1;

    public void Build(IWorkflowBuilder<GlobalContext> builder)
    {
        builder
            .UseDefaultErrorBehavior(WorkflowErrorHandling.Suspend)
            //开始
            .StartWith<Start>()
            .Input(step => step.HumanCommand, ctx => ctx.UserTask)
            .Input(step => step.GlobalContext, ctx => ctx)
            //启动k8s顾问，将用户输入语言，转换为k8s命令
            .Then<ExpertKubernetesConsul>()
            .Input(step => step.GlobalContext, ctx => ctx)
            //提取命令
            .Then<CodeExtractor>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Input(step => step.Pattern, ctx => CodeExtractPattern.SHELL)
            //提取命令
            .Then<CodeExtractor>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Input(step => step.Pattern, ctx => CodeExtractPattern.KUBECTL)
            //运行kubectl
            .Then<KubectlCommandRunner>()
            .Input(step => step.GlobalContext, ctx => ctx)
            //接下来进行循环，直到没有问题，或者超出loop次数
            .Then<LoopBegin>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .While(ctx => ctx.AllowLoop)
            .Do(x => x
                //根据用户诉求、k8s命令的执行结果，综合判断是否需要进行故障排查
                .StartWith<ExpertKubernetesRepair>()
                .Input(step => step.GlobalContext, ctx => ctx)
                //记录执行了一次
                .Then<LoopCountIncrement>()
                .Input(step => step.GlobalContext, ctx => ctx)
                //检测修复专家是否给出了修复建议
                .Then<PassDetector>()
                .Input(step => step.GlobalContext, ctx => ctx)
                //PASS代表无需故障修复，其他需要提取指令运行。
                .If(data => data.LatestMessage.StepResponse != "PASS").Do(then => then
                    .StartWith<CodeExtractor>()
                    .Input(step => step.GlobalContext, ctx => ctx)
                    .Input(step => step.Pattern, ctx => CodeExtractPattern.SHELL)
                    .Then<CodeExtractor>()
                    .Input(step => step.GlobalContext, ctx => ctx)
                    .Input(step => step.Pattern, ctx => CodeExtractPattern.KUBECTL)
                    .Then<KubectlCommandRunner>()
                    .Input(step => step.GlobalContext, ctx => ctx)
                    .Then<Sleep>()
                    .Input(step => step.GlobalContext, ctx => ctx)
                    .Input(step => step.Period, data => TimeSpan.FromSeconds(10))
                )
                .If(data => data.LatestMessage.StepResponse == "PASS").Do(then => then
                    // 返回PASS，结束循环
                    .StartWith<LoopEnd>()
                    .Input(step => step.GlobalContext, ctx => ctx)
                )
            )
            .Then<End>()
            .Input(step => step.GlobalContext, ctx => ctx);
    }
}
