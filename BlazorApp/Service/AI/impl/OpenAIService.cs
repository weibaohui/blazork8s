using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;

namespace BlazorApp.Service.AI.impl;

public class OpenAiService(IConfigService configService, ILogger<OpenAiService> logger) : IOpenAiService
{
    public async Task<string> AIChat(string prompt)
    {
        return await Query(prompt);
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


    public void SetChatEventHandler(EventHandler<string> eventHandler)
    {
        ChatEventHandler += eventHandler;
    }

    public string Name()
    {
        return "OpenAI大模型";
    }

    private string GetToken()
    {
        return configService.GetString("OpenAI", "Token") ?? string.Empty;
    }

    private string GetBaseUrl()
    {
        return configService.GetString("OpenAI", "BaseUrl") ?? string.Empty;
    }

    private string GetModel()
    {
        return configService.GetString("OpenAI", "Model") ?? string.Empty;
    }

    private event EventHandler<string> ChatEventHandler;


    private async Task<string> Query(string prompt)
    {
        var options = new OpenAiOptions()
        {
            ApiKey     = GetToken(),
            BaseDomain = GetBaseUrl()
        };
        var service = new OpenAIService(options);

        var createRequest = new CompletionCreateRequest()
        {
            Prompt = prompt,
            Stream = true
        };
        var resp             = "";
        var completionResult = service.Completions.CreateCompletionAsStream(createRequest, GetModel());

        await foreach (var completion in completionResult)
            if (completion.Successful)
            {
                resp += completion.Choices.FirstOrDefault()?.Text;
                ChatEventHandler?.Invoke(this, completion.Choices.FirstOrDefault()?.Text);
            }
            else
            {
                if (completion.Error == null) logger.LogError("Unknown Error");

                logger.LogError("{ErrorCode}: {ErrorMessage}", completion.Error?.Code, completion.Error?.Message);
            }

        return resp;
    }
}
