using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

/// <summary>
///     启动另一个流程，流程需要提前注册好。
///     将Context传入，那么可以通过Context进行数据传递
/// </summary>
public class RunWorkflow : StepBody
{
    public Context Context { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var workflowName = "Echo";

        Console.WriteLine($"RunWorkflow start:{workflowName}");
        Context.Host.StartWorkflow(workflowName, Context).GetAwaiter().GetResult();
        return ExecutionResult.Next();
    }
}
