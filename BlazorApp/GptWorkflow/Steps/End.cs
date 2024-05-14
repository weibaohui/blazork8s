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
        var currentSubWorkflowName = GlobalContext.CurrentSubWorkflowName;
        if (!string.IsNullOrWhiteSpace(currentSubWorkflowName))
        {
            //结束子流程
            msg.StepResponse = msg.StepInput;
            GlobalContext.Host.PublishEvent(WorkflowConst.SubWorkflowEnd, WorkflowConst.SubWorkflowEnd,
                WorkflowConst.SubWorkflowEnd);
            GlobalContext.Logger.LogDebug("Goodbye SubWorkflow: {SubWorkflowName}", currentSubWorkflowName);
        }
        else
        {
            msg.StepResponse = "Goodbye";
            GlobalContext.Logger.LogDebug("Goodbye world");
        }


        return ExecutionResult.Next();
    }
}
