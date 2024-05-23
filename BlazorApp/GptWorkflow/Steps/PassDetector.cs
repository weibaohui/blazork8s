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
        var text = msg.StepInput.Trim().TrimEnd('.').TrimEnd('。');
        if (text.StartsWith(WorkflowConst.DecideNonePASS) || text.EndsWith(WorkflowConst.DecideNonePASS))
        {
            GlobalContext.DecideResult = WorkflowConst.DecidePASS;
            GlobalContext.Logger.LogDebug("PASS detect: {Text} ====>>>> PASS", text);
            //PASS,清空输出
            msg.StepResponse = string.Empty;
        }
        else
        {
            GlobalContext.DecideResult = WorkflowConst.DecideNonePASS;
            //将输入原样返回，让下一个环节处理
            msg.StepResponse = msg.StepInput;
        }


        return ExecutionResult.Next();
    }
}
