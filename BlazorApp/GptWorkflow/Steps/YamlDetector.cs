using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class YamlDetector : StepBody
{
    private const string StepName = "YamlDetector";

    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);

        GlobalContext.Logger.LogDebug("msg.StepInput={Input}", msg.StepInput);
        var text = msg.StepInput.Trim().TrimEnd('.').TrimEnd('。');
        if (text.ToLower().StartsWith("```yaml") ||
            text.ToLower().StartsWith("yaml"))
        {
            //如果是yaml代码，则将GlobalContext.CodeType设置为Yaml
            GlobalContext.CodeType = WorkflowConst.CodeType.Yaml;
        }
        else if (text.ToLower().StartsWith("```shell") ||
                 text.ToLower().StartsWith("shell"))
        {
            //如果是shell代码，则将GlobalContext.CodeType设置为Shell
            GlobalContext.CodeType = WorkflowConst.CodeType.Shell;
        }

        msg.StepParameter.Add("CodeType", GlobalContext.CodeType);
        //将输入原样返回，让下一个环节处理
        msg.StepResponseIsPassedThrough = true;
        msg.StepResponse = msg.StepInput;


        return ExecutionResult.Next();
    }
}
