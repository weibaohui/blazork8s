using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class End : StepBody
{
    private const string StepName = "End";
    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);
        msg.StepResponse = "Goodbye";
        GlobalContext.Logger.LogDebug("Goodbye world");

        return ExecutionResult.Next();
    }
}
