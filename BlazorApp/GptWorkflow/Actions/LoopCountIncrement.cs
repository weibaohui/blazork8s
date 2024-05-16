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
            msg.StepParameter.Add("LoopControl",
                $"GlobalContext.LoopCount({GlobalContext.LoopCount}) >= GlobalContext.MaxLoopCount({GlobalContext.MaxLoopCount}) => true");
            GlobalContext.AllowLoop = false;
        }

        return ExecutionResult.Next();
    }
}
