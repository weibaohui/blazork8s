using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace BlazorApp.Service.AI.impl;

public class OpenAiService(IConfigService configService, ILogger<OpenAiService> logger) : IOpenAiService
{
    private string GetOpenAiToken()
    {
        return configService.GetString("OpenAI", "Token") ?? string.Empty;
    }

    public async Task<string> AIChat(string prompt)
    {
        return await Query(prompt);
    }

    private async Task<string> Query(string prompt)
    {
        var           options     = new OpenAiOptions() { ApiKey = GetOpenAiToken() };
        OpenAIService service     = new OpenAIService(options);
        var           chatMessage = new ChatMessage(StaticValues.ChatMessageRoles.User, prompt);
        ChatCompletionCreateRequest createRequest = new ChatCompletionCreateRequest()
        {
            Messages = new List<ChatMessage>
            {
                chatMessage
            }
        };

        var res = await service.ChatCompletion
            .CreateCompletion(createRequest, Models.ChatGpt3_5Turbo0301);

        if (res.Successful)
        {
            var ss = res.Choices.FirstOrDefault()?.Message.Content;

            return ss ?? string.Empty;
        }
        else
        {
            logger.LogError("{Error}",KubernetesJson.Serialize(res));
        }

        return string.Empty;
    }


    public async Task<string> ExplainError(string text)
    {
        var prompt  = configService.GetSection("OpenAI")!.GetSection("Prompt").GetValue<string>("error");
        var content = $"{prompt} \n {text}";

        return await Query(content);
    }

    public async Task<string> ExplainSecurity(string text)
    {
        var prompt  = configService.GetSection("OpenAI")!.GetSection("Prompt").GetValue<string>("security");
        var content = $"{prompt} \n {text}";
        return await Query(content);
    }


    public void SetChatEventHandler(EventHandler<string> handler)
    {
        logger.LogInformation("SetChatEventHandler");
    }

    public string Name()
    {
        return "OpenAI大模型";
    }
}
