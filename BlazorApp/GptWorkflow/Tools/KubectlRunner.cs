using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Tools;

public class KubectlRunner : StepBody
{
    private const string StepName = "KubectlRunner";

    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);

        var command = msg.StepInput;

        if (string.IsNullOrWhiteSpace(command))
        {
            return ExecutionResult.Next();
        }

        var ret = "";
        foreach (var cmd in command.Split(";"))
        {
            GlobalContext.Logger.LogDebug("Command: {Cmd}", cmd);
            if (string.IsNullOrWhiteSpace(cmd))
            {
                continue;
            }

            if (cmd.StartsWith("kubectl exec"))
            {
                //交互在自动化场景下没有意义
                continue;
            }
            //
            // if (cmd.StartsWith("kubectl logs") )
            // {
            //     //查看日志、交互在自动化场景下没有意义
            //     continue;
            // }

            ret += $"Run:{cmd}\r\n";
            var exec = cmd.StartsWith("kubectl") ? cmd.Remove(0, "kubectl".Length) : cmd;
            ret += $"Result:\r\n" + GlobalContext.KubectlService.Command(exec).GetAwaiter().GetResult() + "\r\n";
        }

        GlobalContext.Logger.LogDebug("Kubectl final result: {Ret}", ret);
        msg.StepResponse = ret;


        return ExecutionResult.Next();
    }
}
