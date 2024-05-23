using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Actions;

public class LoopBegin : StepBody
{
    private const string StepName = "LoopBegin";

    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        GlobalContext.AllowLoop = true;
        var msg = Message.NewMessage(GlobalContext, StepName);
        msg.StepResponseIsPassedThrough = true;
        msg.StepResponse = msg.StepInput;
        return ExecutionResult.Next();
    }
}
