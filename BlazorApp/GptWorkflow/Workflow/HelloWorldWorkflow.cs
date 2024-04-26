﻿using BlazorApp.GptWorkflow.Steps;
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
            .Input(step => step.HumanCommand, ctx => ctx.UserTask)
            .Input(step => step.Context, ctx => ctx)
            .Then<ExpertKubernetesConsul>()
            .Input(step => step.Context, ctx => ctx)
            .Then<CodeExtract>()
            .Input(step => step.Context, ctx => ctx)
            .Input(step => step.Pattern, ctx => CodeExtractPattern.SHELL)
            .Then<CodeExtract>()
            .Input(step => step.Context, ctx => ctx)
            .Input(step => step.Pattern, ctx => CodeExtractPattern.KUBECTL)
            .Then<Kubectl>()
            .Input(step => step.Context, ctx => ctx)
            .Then<ExpertKubernetesRepair>()
            .Input(step => step.Context, ctx => ctx)
            .Then<PassDetect>()
            .Input(step => step.Context, ctx => ctx)
            .If(data => data.LatestMessage != "PASS").Do(then => then
                .StartWith<CodeExtract>()
                .Input(step => step.Context, ctx => ctx)
                .Input(step => step.Pattern, ctx => CodeExtractPattern.SHELL)
                .Then<CodeExtract>()
                .Input(step => step.Context, ctx => ctx)
                .Input(step => step.Pattern, ctx => CodeExtractPattern.KUBECTL)
                .Then<Kubectl>()
                .Input(step => step.Context, ctx => ctx)
            )
            .Then<GoodbyeWorld>();
    }
}
