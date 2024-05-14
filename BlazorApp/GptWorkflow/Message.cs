using System;
using System.Collections.Generic;

namespace BlazorApp.GptWorkflow;

public class Message
{
    private string _stepResponse;
    public string FlowName { get; set; }
    public string StepName { get; set; }
    public string StepDescription { get; set; }
    public string StepPrompt { get; set; }
    public Dictionary<string, object> StepParameter { get; set; } = new();

    public string StepResponse
    {
        get => _stepResponse;
        set
        {
            _stepResponse = value;
            Ctx.History.Add(value);
            Ctx.OutputEventHandler?.Invoke(this, this);
        }
    }

    public string StepInput { get; set; }

    /// <summary>
    ///     Due date of the task in the format "yyyy-MM-dd HH:mm:ss"
    /// </summary>
    public string StepDueDate { get; set; }

    public string UserTask { get; set; }
    public GlobalContext Ctx { get; set; }


    public static Message NewMessage(GlobalContext ctx, string stepName, string stepDescription = "")
    {
        var msg = new Message
        {
            Ctx = ctx,
            UserTask = ctx.UserTask,
            StepName = stepName,
            StepDescription = stepDescription,
            StepDueDate = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
            //节点的输入默认为上一个step的输出
            StepInput = ctx.LatestMessage.StepResponse
        };
        ctx.LatestMessage = msg;
        if (ctx.AllowLoop)
        {
            msg.StepParameter.Add("AllowLoop", true);
            msg.StepParameter.Add("LoopCount", ctx.LoopCount);
            msg.StepParameter.Add("MaxLoopCount", ctx.MaxLoopCount);
        }

        return msg;
    }
}
