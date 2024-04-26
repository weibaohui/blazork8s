using System;
using BlazorApp.Service.k8s.impl;
using BlazorApp.Utils;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class Kubectl : StepBody
{
    private static readonly ILogger<KubectlService> Logger = LoggingHelper<KubectlService>.Logger();
    private readonly KubectlService _kubectlService = new KubectlService(Logger);
    public string Command { get; set; }
    public string Result { get; set; }

    public Context Context { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        foreach (var cmd in Command.Split(";"))
        {
            Console.WriteLine($"Command: {cmd}");
            if (cmd.StartsWith("kubectl logs") || cmd.StartsWith("kubectl exec"))
            {
                //查看日志、交互在自动化场景下没有意义
                continue;
            }

            var exec = cmd.StartsWith("kubectl") ? cmd.Remove(0, "kubectl".Length) : cmd;
            Result += _kubectlService.Command(exec).GetAwaiter().GetResult() + "\r\n";
        }

        Console.WriteLine($"Kubectl final result: {Result}");
        Context.History.Add(Result);
        return ExecutionResult.Next();
    }
}
