using BlazorApp.GptWorkflow.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Workflow;

public class HelloWorldWorkflow : IGptWorkflow<Context>
{
    public static string Name => "HelloWorld";
    public string Id => "HelloWorld";
    public int Version => 1;

    public void Build(IWorkflowBuilder<Context> builder)
    {
        builder
            .UseDefaultErrorBehavior(WorkflowErrorHandling.Suspend)
            .StartWith<HelloWorld>()
            .Then<Kubectl>()
            .Input(step => step.Command, ctx => ctx.Command)
            .Output(ctx => ctx.Result, step => step.Result)
            .Then<OpenAi>()
            .Input(step => step.Text, ctx => ctx.Result)
            .Input(step => step.Prompt, ctx => ctx.Prompt)
            .Input(setp => setp.Ai, ctx => ctx.AiService)
            .Output(ctx => ctx.Result, step => step.Result)
            .Then<GoodbyeWorld>();
    }
}
