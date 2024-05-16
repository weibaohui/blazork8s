using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Tools;

public class KubectlYamlApplier : StepBody
{
    private const string StepName = "KubectlYamlApplier";

    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);

        var yaml = msg.StepInput;

        if (string.IsNullOrWhiteSpace(yaml)) return ExecutionResult.Next();


        yaml = yaml.Trim('`', '\r', '\n', ';', '.', '。');
        yaml = yaml.Trim('`', '\r', '\n', ';', '.', '。');
        yaml = yaml.Trim('`', '\r', '\n', ';', '.', '。');
        //针对头部有yaml字符串的情况进行处理
        if (yaml.StartsWith("yaml"))
        {
            yaml = yaml.Substring(4);
        }

        GlobalContext.Logger.LogDebug("Yaml: {Yaml}", yaml);

        var ret = $"";
        ret += $"Result:\r\n{GlobalContext.KubectlService.Apply(yaml).GetAwaiter().GetResult()}\r\n";

        GlobalContext.Logger.LogDebug("Kubectl apply result: {Ret}", ret);
        msg.StepResponse = ret;


        return ExecutionResult.Next();
    }
}
