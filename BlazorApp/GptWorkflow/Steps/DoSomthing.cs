using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class DoSomething : StepBody
{
    public Context Context { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine($"DoSomething LatestMessage={Context.LatestMessage}");
        Context.History.Add("DoSomething DoSomething DoSomething DoSomething");
        return ExecutionResult.Next();
    }
}
