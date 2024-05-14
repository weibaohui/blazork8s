using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Actions;

public class LoopCountIncrement : StepBody
{
    private const string StepName = "LoopCountIncrement";

    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);
        msg.StepResponse = msg.StepInput;
        GlobalContext.AllowLoop = true;
        GlobalContext.LoopCount += 1;
        if (GlobalContext.LoopCount >= GlobalContext.MaxLoopCount)
        {
            GlobalContext.AllowLoop = false;
        }

        return ExecutionResult.Next();
    }
}
