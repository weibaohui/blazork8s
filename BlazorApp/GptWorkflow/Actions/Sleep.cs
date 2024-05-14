using System;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Actions;

public class Sleep : StepBody
{
    private const string StepName = "Sleep";
    public GlobalContext GlobalContext { get; set; }

    /// <summary>
    /// Example: Period = TimeSpan.FromSeconds(20)
    /// </summary>
    public TimeSpan Period { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        GlobalContext.Logger.LogDebug("Start:Hello world");
        var msg = Message.NewMessage(GlobalContext, StepName);
        msg.StepParameter.Add("Sleep", "True");
        msg.StepParameter.Add("Period", Period);
        msg.StepResponse = msg.StepInput;
        if (context.PersistenceData == null)
            return ExecutionResult.Sleep(Period, new object());
        return ExecutionResult.Next();
    }
}
