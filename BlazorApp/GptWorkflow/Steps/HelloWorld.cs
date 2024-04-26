using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class HelloWorld : StepBody
{
    public Context Context { get; set; }
    public string HumanCommand { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Context.LatestMessage = HumanCommand;
        Console.WriteLine("Hello world");
        return ExecutionResult.Next();
    }
}
