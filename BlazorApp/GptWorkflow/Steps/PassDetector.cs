using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class PassDetector : StepBody
{
    private const string StepName = "PassDetector";

    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);

        GlobalContext.Logger.LogDebug("msg.StepInput={Input}", msg.StepInput);
        var text = msg.StepInput.Trim();
        if (text.StartsWith("PASS") || text.StartsWith("PASS"))
        {
            msg.StepResponse = "PASS";
            GlobalContext.Logger.LogDebug("PASS detect: {Text} ====>>>> PASS", text);
        }
        else
        {
            //如果不是PASS，那么需要将输入原样返回，让下一个环节处理
            msg.StepResponse = msg.StepInput;
        }


        return ExecutionResult.Next();
    }
}
