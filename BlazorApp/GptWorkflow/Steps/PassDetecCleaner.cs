using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class PassDetectorCleaner : StepBody
{
    private const string StepName = "PassDetectorCleaner";

    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);
        GlobalContext.DecideResult = string.Empty;
        //将输入原样返回，让下一个环节处理
        msg.StepResponseIsPassedThrough = true;
        msg.StepResponse = msg.StepInput;


        return ExecutionResult.Next();
    }
}
