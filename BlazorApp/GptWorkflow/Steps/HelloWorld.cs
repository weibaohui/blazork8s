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
        Context.OutputEventHandler.Invoke(this, Context.LatestMessage);

        Console.WriteLine("Hello world");
        return ExecutionResult.Next();
    }
}