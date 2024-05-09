using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class KubectlRunner : StepBody
{
    private const string StepName = "KubectlRunner";

    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);

        var command = msg.StepInput;
        var ret = "";
        foreach (var cmd in command.Split(";"))
        {
            GlobalContext.Logger.LogDebug("Command: {Cmd}", cmd);
            if (cmd.StartsWith("kubectl logs") || cmd.StartsWith("kubectl exec") || string.IsNullOrWhiteSpace(cmd))
            {
                //查看日志、交互在自动化场景下没有意义
                continue;
            }

            ret += $"Run:{cmd}\r\n";
            var exec = cmd.StartsWith("kubectl") ? cmd.Remove(0, "kubectl".Length) : cmd;
            ret += $"Result:\r\n" + GlobalContext.KubectlService.Command(exec).GetAwaiter().GetResult() + "\r\n";
        }

        GlobalContext.Logger.LogDebug("Kubectl final result: {Ret}", ret);
        msg.StepResponse = ret;

        GlobalContext.LatestMessage = msg;

        return ExecutionResult.Next();
    }
}
