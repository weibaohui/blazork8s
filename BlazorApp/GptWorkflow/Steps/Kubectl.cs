using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class Kubectl : StepBody
{
    public Context Context { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var command = Context.LatestMessage;
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
            ret += Context.KubectlService.Command(exec).GetAwaiter().GetResult() + "\r\n";
        }

        Console.WriteLine($"Kubectl final result: {ret}");
        Context.LatestMessage = ret;
        Context.History.Add(Context.LatestMessage);
        return ExecutionResult.Next();
    }
}
