﻿using WorkflowCore.Interface;
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
        GlobalContext.CurrentSubWorkflowName = WorkflowName;

        var lastMsg = GlobalContext.LatestMessage;
        var msg = Message.NewMessage(GlobalContext, StepName);
        msg.StepParameter.Add("SubWorkflowName", WorkflowName);

        GlobalContext.Host.StartWorkflow(WorkflowName, GlobalContext).GetAwaiter().GetResult();

        //使用上一个步骤，因为本步骤只是启动另一个流程，并不关心结果。
        GlobalContext.LatestMessage = lastMsg;
        return ExecutionResult.Next();
    }
}
