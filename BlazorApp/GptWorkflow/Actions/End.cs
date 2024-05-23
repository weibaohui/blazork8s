using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Actions;

public class End : StepBody
{
    private const string StepName = "End";
    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);
        var currentSubWorkflowName = GlobalContext.CurrentSubWorkflowName;
        if (!string.IsNullOrWhiteSpace(currentSubWorkflowName))
        {
            //结束子流程
            GlobalContext.Host.PublishEvent(WorkflowConst.SubWorkflowEnd, WorkflowConst.SubWorkflowEnd,
                WorkflowConst.SubWorkflowEnd);
            msg.StepParameter.Add("End", currentSubWorkflowName);
        }
        else
        {
            msg.StepParameter.Add("End", GlobalContext.CurrentWorkflowName);
        }

        msg.StepResponseIsPassedThrough = true;
        msg.StepResponse = msg.StepInput;

        return ExecutionResult.Next();
    }
}
