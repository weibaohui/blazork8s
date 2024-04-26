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

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Result = _kubectlService.Command(Command).GetAwaiter().GetResult();
        Console.WriteLine(Result);
        return ExecutionResult.Next();
    }
}
