using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class OpenAi() : StepBody
{
    private string _prompt;
    public Context Context { get; set; }

    private bool Check()
    {
        if (Context.AiService == null)
        {
            Context.Logger.LogError("OpenAi Null ERROR");
            return false;
        }

        return true;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        if (Check())
        {
            _prompt = $"{Context.LatestMessage}\n请根据以上内容，{_prompt}";
            Context.Logger.LogInformation("OpenAi Prompt: {Prompt}", _prompt);
            Context.LatestMessage = Context.AiService?.AIChat(_prompt).GetAwaiter().GetResult();
            Context.Logger.LogInformation("OpenAi Result: {Result}", Context.LatestMessage);
            Context.History.Add(Context.LatestMessage);
            Context.OutputEventHandler.Invoke(this, Context.LatestMessage);
        }


        return ExecutionResult.Next();
    }
}
