using BlazorApp.GptWorkflow.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Workflow;

public class EchoWorkflow : IGptWorkflow<Context>
{
    public static string Name => "Echo";
    public string Id => "Echo";
    public int Version => 1;

    public void Build(IWorkflowBuilder<Context> builder)
    {
        builder
            .UseDefaultErrorBehavior(WorkflowErrorHandling.Suspend)
            .StartWith<DoSomething>()
            .Input(step => step.Context, ctx => ctx)
            .Then<GoodbyeWorld>();
    }
}
