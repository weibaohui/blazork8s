using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class SubWorkflowCleaner : StepBody
{
    private const string StepName = "SubWorkflowCleaner";

    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);
        GlobalContext.CurrentSubWorkflowName = null;
        //将输入原样返回，让下一个环节处理
        msg.StepResponseIsPassedThrough = true;
        msg.StepResponse = msg.StepInput;

        return ExecutionResult.Next();
    }
}
