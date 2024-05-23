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

        var text = msg.StepInput.Trim().TrimEnd('.').TrimEnd('。');
        text = text.Replace("```shell", "")
            .Replace("```bash", "")
            .Replace("```python", "")
            .Replace("```yaml", "")
            .Replace("```", "")
            .Trim();
        if (text.StartsWith(WorkflowConst.DecidePASS) || text.EndsWith(WorkflowConst.DecidePASS))
        {
            GlobalContext.DecideResult = WorkflowConst.DecidePASS;
            msg.StepParameter.Add("DecideResult", GlobalContext.DecideResult);
            GlobalContext.Logger.LogDebug("PASS detect: {Text} ====>>>> PASS", text);
        }
        else
        {
            GlobalContext.DecideResult = WorkflowConst.DecideNonePASS;
            msg.StepParameter.Add("DecideResult", GlobalContext.DecideResult);
        }

        //将输入原样返回，让下一个环节处理
        msg.StepResponseIsPassedThrough = true;
        msg.StepResponse = msg.StepInput;

        return ExecutionResult.Next();
    }
}
