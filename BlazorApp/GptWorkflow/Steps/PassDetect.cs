using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class PassDetect : StepBody
{
    private const string StepName = "PassDetect";

    public GlobalContext GlobalContext { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);

        Console.WriteLine($"msg.UserTask={msg.UserTask}");
        Console.WriteLine($"msg.StepInput={msg.StepInput}");
        var text = msg.StepInput;
        if (text.StartsWith("PASS"))
        {
            msg.StepResponse = "PASS";
            Console.WriteLine($"PASS detect: {text} ====>>>> PASS");
        }
        else
        {
            //如果不是PASS，那么需要将输入原样返回，让下一个环节处理
            msg.StepResponse = msg.StepInput;
        }

        GlobalContext.LatestMessage = msg;

        return ExecutionResult.Next();
    }
}
