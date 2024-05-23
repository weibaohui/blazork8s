using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Actions;

public class BranchRunEnd : StepBody
{
    private const string StepName = "BranchRunEnd";
    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);
        //结束子流程
        msg.StepResponseIsPassedThrough = true;
        msg.StepResponse = msg.StepInput;
        GlobalContext.Host.PublishEvent(WorkflowConst.BranchRunEnd, WorkflowConst.BranchRunEnd,
            WorkflowConst.BranchRunEnd);
        msg.StepParameter.Add("End", "BranchRunEnd");

        return ExecutionResult.Next();
    }
}
