using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class SomethingRunner : StepBody
{
    private const string StepName = "DoSomething";

    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);
        GlobalContext.Logger.LogDebug("DoSomething Input={Input}", msg.StepInput);
        // Do something with the input，and set the output to the message
        msg.StepResponse = msg.StepInput;

        return ExecutionResult.Next();
    }
}
