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

    public async Task<string> Explain(string text)
    {
        // Console.WriteLine(text);
        var enable = _configService.GetBool("OpenAI", "Enable");
        if (!enable)
        {
            return string.Empty;
        }

        var openapiToken = _configService.GetString("OpenAI", "Token");
        var prompt       = _configService.GetSection("OpenAI")!.GetSection("Prompt").GetValue<string>("error");
        // Console.WriteLine(prompt);
        OpenAIService service     = new OpenAIService(new OpenAiOptions() { ApiKey = openapiToken });
        var           chatMessage = new ChatMessage(StaticValues.ChatMessageRoles.User, $"{prompt} \n {text}");
        ChatCompletionCreateRequest createRequest = new ChatCompletionCreateRequest()
        {
            Messages = new List<ChatMessage>
            {
                chatMessage
            }
        };

        var res = await service.ChatCompletion
            .CreateCompletion(createRequest, Models.ChatGpt3_5Turbo);

        // Console.WriteLine(res.Successful);
        // Console.WriteLine(res.ToString());
        if (res.Successful)
        {
            var ss = res.Choices.FirstOrDefault()?.Message.Content;
            // Console.WriteLine(ss);
            return ss;
        }

        return string.Empty;
    }
}
