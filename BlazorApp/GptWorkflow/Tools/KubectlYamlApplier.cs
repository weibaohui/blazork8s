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

        GlobalContext.Logger.LogDebug("Yaml: {Yaml}", yaml);

        var ret = $"Apply:{yaml}\r\n";
        ret += $"Result:\r\n{GlobalContext.KubectlService.Apply(yaml).GetAwaiter().GetResult()}\r\n";

        GlobalContext.Logger.LogDebug("Kubectl apply result: {Ret}", ret);
        msg.StepResponse = ret;


        return ExecutionResult.Next();
    }
}
