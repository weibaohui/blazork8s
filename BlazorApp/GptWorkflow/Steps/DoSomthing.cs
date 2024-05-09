using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class DoSomething : StepBody
{
    private const string StepName = "DoSomething";

    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);
        Console.WriteLine($"DoSomething LatestMessage={msg.StepInput}");
        // Do something with the input，and set the output to the message
        msg.StepResponse = msg.StepInput;
        GlobalContext.LatestMessage = msg;
        return ExecutionResult.Next();
    }
}
