using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class OpenAi() : StepBody
{
    private const string StepName = "OpenAi";

    private string _prompt;
    public GlobalContext GlobalContext { get; set; }

    private bool Check()
    {
        if (GlobalContext.AiService == null)
        {
            GlobalContext.Logger.LogError("OpenAi Null ERROR");
            return false;
        }

        return true;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);
        if (Check())
        {
            _prompt = $"{msg.StepInput}\n请根据以上内容，{_prompt}";
            msg.StepPrompt = _prompt;

            GlobalContext.Logger.LogInformation("OpenAi Prompt: {Prompt}", _prompt);
            msg.StepResponse = GlobalContext.AiService?.AIChat(_prompt).GetAwaiter().GetResult();
            GlobalContext.Logger.LogInformation("OpenAi Result: {Result}", msg.StepResponse);
        }

        GlobalContext.LatestMessage = msg;
        return ExecutionResult.Next();
    }
}
