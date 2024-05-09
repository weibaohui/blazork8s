using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class Kubectl : StepBody
{
    private const string StepName = "Kubectl";

    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);

        var command = msg.StepInput;
        var ret = "";
        foreach (var cmd in command.Split(";"))
        {
            Console.WriteLine($"Command: {cmd}");
            if (cmd.StartsWith("kubectl logs") || cmd.StartsWith("kubectl exec"))
            {
                //查看日志、交互在自动化场景下没有意义
                continue;
            }

            var exec = cmd.StartsWith("kubectl") ? cmd.Remove(0, "kubectl".Length) : cmd;
            ret += GlobalContext.KubectlService.Command(exec).GetAwaiter().GetResult() + "\r\n";
        }

        Console.WriteLine($"Kubectl final result: {ret}");
        msg.StepResponse = ret;

        GlobalContext.LatestMessage = msg;

        return ExecutionResult.Next();
    }
}
