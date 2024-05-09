using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

/// <summary>
///     启动另一个流程，流程需要提前注册好。
///     将Context传入，那么可以通过Context进行数据传递
/// </summary>
public class SubWorkflowRunner : StepBody
{
    private const string StepName = "SubWorkflowRunner";

    public GlobalContext GlobalContext { get; set; }
    public string WorkflowName { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);
        msg.StepParameter.Add("WorkflowName", WorkflowName);

        GlobalContext.Logger.LogDebug("RunWorkflow start:{Name}", WorkflowName);
        GlobalContext.Host.StartWorkflow(WorkflowName, GlobalContext).GetAwaiter().GetResult();
        msg.StepResponse = $"RunWorkflow {WorkflowName} success";

        //不要传递msg，让下一个步骤，使用上一个步骤，因为本步骤只是启动另一个流程，并不关心结果。
        // GlobalContext.LatestMessage = msg;
        return ExecutionResult.Next();
    }
}
