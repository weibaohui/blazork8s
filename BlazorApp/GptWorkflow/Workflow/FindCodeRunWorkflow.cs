using BlazorApp.GptWorkflow.Actions;
using BlazorApp.GptWorkflow.Steps;
using BlazorApp.GptWorkflow.Tools;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Workflow;

public class FindCodeRunWorkflow : IGptWorkflow<GlobalContext>
{
    public static string Name => "FindCodeRunWorkflow";
    public string Id => "FindCodeRunWorkflow";
    public int Version => 1;

    public void Build(IWorkflowBuilder<GlobalContext> builder)
    {
        builder
            .UseDefaultErrorBehavior(WorkflowErrorHandling.Suspend)
            .StartWith<SomethingRunner>()
            .Input(step => step.GlobalContext, ctx => ctx)
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
            .Then<End>()
            .Input(step => step.GlobalContext, ctx => ctx);
        ;
    }
}
