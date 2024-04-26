using BlazorApp.Service.AI;
using BlazorApp.Utils;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class OpenAi() : StepBody
{
    private readonly ILogger<OpenAi> _logger = LoggingHelper<OpenAi>.Logger();
    public string Prompt { get; set; }
    public string Text { get; set; }
    public string Result { get; set; }
    public IAiService Ai { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        if (Ai == null)
        {
            _logger.LogError("OpenAi Null ERROR");
        }

        Prompt = $"{Text}\n请根据以上内容，{Prompt}";
        _logger.LogInformation("OpenAi Prompt: {Prompt}", Prompt);
        Result = Ai?.AIChat(Prompt).GetAwaiter().GetResult();
        _logger.LogInformation("OpenAi Result: {Result}", Result);
        return ExecutionResult.Next();
    }
}
