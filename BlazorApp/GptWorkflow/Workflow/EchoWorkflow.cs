using BlazorApp.GptWorkflow.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Workflow;

public class EchoWorkflow : IGptWorkflow<GlobalContext>
{
    public static string Name => "Echo";
    public string Id => "Echo";
    public int Version => 1;

    public void Build(IWorkflowBuilder<GlobalContext> builder)
    {
        builder
            .UseDefaultErrorBehavior(WorkflowErrorHandling.Suspend)
            .StartWith<DoSomething>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Then<End>();
    }
}
