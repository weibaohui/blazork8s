using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class ExpertKubernetesConsul : StepBody
{
    private const string StepName = "ExpertKubernetesConsul";


    private string _prompt = "请你作为一个k8s专家、高级管理员的角色，面对上述用户的目标。\n" +
                             "请你针对用户给出的目标，返回一条最有可能达成目标的命令。\n" +
                             "要求命令用```shell ```包裹起来。命令必须是可以直接执行的、正确的命令。\n" +
                             "请注意使用<br>换行.请作答";

    public GlobalContext GlobalContext { get; set; }

    private bool Check()
    {
        if (GlobalContext.AiService == null)
        {
            GlobalContext.Logger.LogError("ExpertKubernetes AIService ERROR Null");
            return false;
        }

        return true;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);

        if (Check())
        {
            _prompt = $"{msg.StepInput}\n{_prompt}";
            msg.StepPrompt = _prompt;
            GlobalContext.Logger.LogDebug("ExpertKubernetes Prompt: {Prompt}", _prompt);
            msg.StepResponse = GlobalContext.AiService?.AIChat(_prompt).GetAwaiter().GetResult();
            GlobalContext.Logger.LogDebug("ExpertKubernetes Result: {Result}", msg.StepResponse);
        }


        return ExecutionResult.Next();
    }
}
