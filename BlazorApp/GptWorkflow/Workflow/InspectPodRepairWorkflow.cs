using BlazorApp.GptWorkflow.Actions;
using BlazorApp.GptWorkflow.Steps;
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
        var branchNonePass = builder.CreateBranch()
            .StartWith<SubWorkflowRunner>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Input(step => step.WorkflowName, ctx => FindCodeRunWorkflow.Name)
            .WaitFor(WorkflowConst.SubWorkflowEnd, ctx => WorkflowConst.SubWorkflowEnd);
        builder
            .UseDefaultErrorBehavior(WorkflowErrorHandling.Suspend)
            //开始
            .StartWith<Start>()
            .Input(step => step.HumanCommand, ctx => ctx.UserTask)
            .Input(step => step.GlobalContext, ctx => ctx)
            .Input(step => step.WorkflowName, ctx => InspectPodRepairWorkflow.Name)
            //启动k8s顾问，将用户输入语言，转换为k8s命令
            .Then<ExpertKubernetesConsul>()
            .Input(step => step.GlobalContext, ctx => ctx)
            //启动命令提取执行子流程
            .Then<SubWorkflowRunner>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Input(step => step.WorkflowName, ctx => FindCodeRunWorkflow.Name)
            .WaitFor(WorkflowConst.SubWorkflowEnd, ctx => WorkflowConst.SubWorkflowEnd)
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
            .Decide(data => data.DecideResult)
            .Branch(WorkflowConst.DecideNonePASS, branchNonePass)
            .Then<End>()
            .Input(step => step.GlobalContext, ctx => ctx);
    }
}
