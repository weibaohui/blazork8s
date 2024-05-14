using BlazorApp.GptWorkflow.Actions;
using BlazorApp.GptWorkflow.Steps;
using BlazorApp.GptWorkflow.Tools;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Workflow;

public class InspectPodRepairWorkflow : IGptWorkflow<GlobalContext>
{
    public static string Name => "InspectPodRepairWorkflow";
    public string Id => "InspectPodRepairWorkflow";
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
            .Then<KubectlRunner>()
            .Input(step => step.GlobalContext, ctx => ctx)
            //根据用户诉求、k8s命令的执行结果，综合判断是否需要进行故障排查
            .Then<ExpertKubernetesRepair>()
            .Input(step => step.GlobalContext, ctx => ctx)
            //启动一个子处理流程，本处用于测试
            .Then<SubWorkflowRunner>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Input(step => step.WorkflowName, ctx => "Echo")
            .WaitFor(WorkflowConst.SubWorkflowEnd, ctx => WorkflowConst.SubWorkflowEnd)
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
                .Then<KubectlRunner>()
                .Input(step => step.GlobalContext, ctx => ctx)
            )
            .Then<End>()
            .Input(step => step.GlobalContext, ctx => ctx);
    }
}
