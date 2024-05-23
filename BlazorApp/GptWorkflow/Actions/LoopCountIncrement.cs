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
        if (GlobalContext.LoopCount >= GlobalContext.MaxLoopCount)
        {
            msg.StepParameter.Add("LoopControl",
                $"GlobalContext.LoopCount({GlobalContext.LoopCount}) >= GlobalContext.MaxLoopCount({GlobalContext.MaxLoopCount}) => true");
            GlobalContext.AllowLoop = false;
        }

        GlobalContext.LoopCount += 1;
        return ExecutionResult.Next();
    }
}
