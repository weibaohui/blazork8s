using BlazorApp.GptWorkflow.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Workflow;

public class InspectPodRepairWorkflow : IGptWorkflow<GlobalContext>
{
    public static string Name => "Start";
    public string Id => "Start";
    public int Version => 1;

    public void Build(IWorkflowBuilder<GlobalContext> builder)
    {
        builder
            .UseDefaultErrorBehavior(WorkflowErrorHandling.Suspend)
            .StartWith<Start>()
            .Input(step => step.HumanCommand, ctx => ctx.UserTask)
            .Input(step => step.GlobalContext, ctx => ctx)
            .Then<ExpertKubernetesConsul>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Then<CodeExtract>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Input(step => step.Pattern, ctx => CodeExtractPattern.SHELL)
            .Then<CodeExtract>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Input(step => step.Pattern, ctx => CodeExtractPattern.KUBECTL)
            .Then<KubectlRunner>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Then<ExpertKubernetesRepair>()
            .Input(step => step.GlobalContext, ctx => ctx)
            // .Then<SubWorkflowRunner>()
            // .Input(step => step.GlobalContext, ctx => ctx)
            // .Input(step => step.WorkflowName, ctx => "Echo")
            .Then<PassDetector>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .If(data => data.LatestMessage.StepResponse != "PASS").Do(then => then
                .StartWith<CodeExtract>()
                .Input(step => step.GlobalContext, ctx => ctx)
                .Input(step => step.Pattern, ctx => CodeExtractPattern.SHELL)
                .Then<CodeExtract>()
                .Input(step => step.GlobalContext, ctx => ctx)
                .Input(step => step.Pattern, ctx => CodeExtractPattern.KUBECTL)
                .Then<KubectlRunner>()
                .Input(step => step.GlobalContext, ctx => ctx)
            )
            .Then<End>()
            .Input(step => step.GlobalContext, ctx => ctx);
    }
}
