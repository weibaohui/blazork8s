using System;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class Start : StepBody
{
    private const string StepName = "Start";

    public GlobalContext GlobalContext { get; set; }
    public string HumanCommand { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        //这是流程起点，没有上一个步骤，所以不需要获取上一步的输出。
        //但是后面的节点，都需要上一个步骤，因此将本处的msg，作为全局Context的属性，供后续步骤使用。
        var msg = new Message
        {
            Ctx = GlobalContext,
            UserTask = GlobalContext.UserTask,
            StepName = StepName,
            StepDueDate = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
            StepInput = HumanCommand,
            StepResponse = HumanCommand
        };
        GlobalContext.LatestMessage = msg;
        GlobalContext.Logger.LogDebug("Start:Hello world");
        return ExecutionResult.Next();
    }
}
