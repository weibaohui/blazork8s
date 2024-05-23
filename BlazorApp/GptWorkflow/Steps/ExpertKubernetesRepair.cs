using System;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class ExpertKubernetesRepair : StepBody
{
    private const string StepName = "ExpertKubernetesRepair";

    private string _prompt = "请你作为一个k8s专家,根据以上内容,判断运行是否有问题。" +
                             "如果没有问题，请你返回PASS这个单词，请务必注意只能返回PASS这4个字符。" +
                             "如果有问题请：\n" +
                             "0、列出有问题的资源名称（如Pod、Deployment等资源）,请确保资源名称是从给你的内容中提取的，并保证一行一个。\n" +
                             "1、列出最主要的1个问题。\n" +
                             "2、详细解释下这个问题发生的根本原因。\n" +
                             "3、列出可能得解决方案。\n" +
                             "5、列出你准备执行的命令，要求命令用```shell ```包裹起来。命令必须是可以直接执行的、正确的命令。\n" +
                             "5.1、如果你要查看日志，请只列出最近的100条日志。绝对不要输出所有的日志。\n" +
                             "5.2、如果你需要执行kubectl  exec命令，请输提示文字请求人类手动执行。然后你返回PASS这4个字符。请务必注意只能返回PASS这4个字符。。\n" +
                             "6、如果你准备执行的命令不是一个完整的命令、配置等或者你认为这个命令并不能真正有效的解决问题，那么请放弃，并请你返回PASS这4个字符。请务必注意只能返回PASS这4个字符。\n" +
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
        var input = GlobalContext.LatestMessage.StepResponse;
        Console.WriteLine($"GlobalContext.LatestMessage {GlobalContext.LatestMessage.StepName}");

        var msg = Message.NewMessage(GlobalContext, StepName);

        if (Check())
        {
            _prompt = $"用户诉求：{msg.UserTask}\n获得信息：{msg.StepInput}\n{_prompt}";
            msg.StepPrompt = _prompt;
            GlobalContext.Logger.LogDebug("ExpertKubernetes Prompt: {Prompt}", _prompt);
            msg.StepResponse = GlobalContext.AiService?.AIChat(_prompt).GetAwaiter().GetResult();
            GlobalContext.Logger.LogDebug("ExpertKubernetes Result: {Result}", msg.StepResponse);
        }


        return ExecutionResult.Next();
    }
}
