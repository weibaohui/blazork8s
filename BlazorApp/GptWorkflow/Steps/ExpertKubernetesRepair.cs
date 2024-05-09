using BlazorApp.Utils;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class ExpertKubernetesRepair : StepBody
{
    private const string StepName = "ExpertKubernetesRepair";
    private readonly ILogger<OpenAi> _logger = LoggingHelper<OpenAi>.Logger();

    private string _prompt = "请你作为一个k8s专家,根据以上内容,判断运行是否有问题。" +
                             "如果没有问题，请返回PASS这个单词，请务必注意只能返回PASS这4个字符。" +
                             "如果有问题请：\n" +
                             "0、列出有问题的资源名称（如Pod、Deployment等资源）,请确保资源名称是从给你的内容中提取的，并保证一行一个。\n" +
                             "1、列出最主要的1个问题。\n" +
                             "2、详细解释下这个问题发生的根本原因。\n" +
                             "3、列出可能得解决方案。\n" +
                             "4、请根据解决方案，推断用出修复命令。\n" +
                             "5、列出你准备执行的命令，要求命令用```shell ```包裹起来。命令必须是可以直接执行的、正确的命令。\n" +
                             "请注意使用<br>换行.请作答";

    public GlobalContext GlobalContext { get; set; }

    private bool Check()
    {
        if (GlobalContext.AiService == null)
        {
            _logger.LogError("ExpertKubernetes AIService ERROR Null");
            return false;
        }

        return true;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName);
        if (Check())
        {
            _prompt = $"用户诉求：{msg.UserTask}\n获得信息：{msg.StepInput}\n{_prompt}";
            msg.StepPrompt = _prompt;
            _logger.LogInformation("ExpertKubernetes Prompt: {Prompt}", _prompt);
            msg.StepResponse = GlobalContext.AiService?.AIChat(_prompt).GetAwaiter().GetResult();
            _logger.LogInformation("ExpertKubernetes Result: {Result}", msg.StepResponse);
        }

        GlobalContext.LatestMessage = msg;
        return ExecutionResult.Next();
    }
}
