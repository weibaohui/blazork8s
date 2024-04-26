using BlazorApp.Utils;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class ExpertKubernetesConsul : StepBody
{
    private readonly ILogger<OpenAi> _logger = LoggingHelper<OpenAi>.Logger();

    private string _prompt = "请你作为一个k8s专家、高级管理员的角色，面对上述用户的目标。\n" +
                             "请你针对用户给出的目标，返回一条最有可能达成目标的命令。\n" +
                             "要求命令用```shell ```包裹起来。命令必须是可以直接执行的、正确的命令。\n" +
                             "请注意使用<br>换行.请作答";

    public Context Context { get; set; }

    private bool Check()
    {
        if (Context.AiService == null)
        {
            _logger.LogError("ExpertKubernetes AIService ERROR Null");
            return false;
        }

        return true;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        if (Check())
        {
            _prompt = $"{Context.LatestMessage}\n{_prompt}";
            _logger.LogInformation("ExpertKubernetes Prompt: {Prompt}", _prompt);
            Context.LatestMessage = Context.AiService?.AIChat(_prompt).GetAwaiter().GetResult();
            _logger.LogInformation("ExpertKubernetes Result: {Result}", Context.LatestMessage);
            Context.History.Add(Context.LatestMessage);
        }


        return ExecutionResult.Next();
    }
}
