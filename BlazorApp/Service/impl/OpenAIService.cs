using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace BlazorApp.Service.impl;

public class OpenAiService : IOpenAiService
{
    private readonly IConfigService _configService;

    public OpenAiService(IConfigService configService)
    {
        _configService = configService;
    }

    public bool Enabled()
    {
        return IsOpenAiEnabled();
    }

    private string GetOpenAiToken()
    {
        return _configService.GetString("OpenAI", "Token") ?? string.Empty;
    }

    public async Task<string> Chat(string prompt)
    {
        if (!IsOpenAiEnabled())
        {
            return string.Empty;
        }

        return await Query(prompt);
    }

    private async Task<string> Query(string prompt)
    {
        Console.WriteLine($"OpenAI:prompt{prompt}");
        OpenAIService service     = new OpenAIService(new OpenAiOptions() { ApiKey = GetOpenAiToken() });
        var           chatMessage = new ChatMessage(StaticValues.ChatMessageRoles.User, prompt);
        ChatCompletionCreateRequest createRequest = new ChatCompletionCreateRequest()
        {
            Messages = new List<ChatMessage>
            {
                chatMessage
            }
        };

        var res = await service.ChatCompletion
            .CreateCompletion(createRequest, Models.ChatGpt3_5Turbo);

        Console.WriteLine(res.Successful);
        Console.WriteLine(res.ToString());
        if (res.Successful)
        {
            var ss = res.Choices.FirstOrDefault()?.Message.Content;
            Console.WriteLine(ss);

            return ss ?? string.Empty;
        }

        return string.Empty;
    }

    private bool IsOpenAiEnabled()
    {
        return _configService.GetBool("OpenAI", "Enable");
    }

    public async Task<string> ExplainError(string text)
    {
        if (!IsOpenAiEnabled())
        {
            return string.Empty;
        }

        var prompt  = _configService.GetSection("OpenAI")!.GetSection("Prompt").GetValue<string>("error");
        var content = $"{prompt} \n {text}";
        return await Query(content);
    }

    public async Task<string> ExplainSecurity(string text)
    {
        if (!IsOpenAiEnabled())
        {
            return string.Empty;
        }

        var prompt  = _configService.GetSection("OpenAI")!.GetSection("Prompt").GetValue<string>("security");
        var content = $"{prompt} \n {text}";
        return await Query(content);
    }
}
