using System;
using System.Threading.Tasks;

namespace BlazorApp.Service.AI.impl;

public class AiService(
    IConfigService   configService,
    IQwenAiService   qwenAi,
    IXunFeiAiService xunFeiAi,
    IOpenAiService   openAi,
    IGeminiAiService geminiAi
) : IAiService
{
    public bool Enabled()
    {
        return configService.GetBool("AI", "Enable");
    }

    public Task<string> ExplainError(string text)
    {
        return GetAi().ExplainError(text);
    }

    public Task<string> ExplainSecurity(string text)
    {
        return GetAi().ExplainSecurity(text);
    }


    public string Selected()
    {
        return configService.GetString("AI", "Select") ?? string.Empty;
    }

    public string Name()
    {
        return GetAi().Name();
    }

    public Task<string> AIChat(string text)
    {
        return GetAi().AIChat(text);
    }

    public void SetChatEventHandler(EventHandler<string> handler)
    {
        GetAi().SetChatEventHandler(handler);
    }

    private IAiService GetAi()
    {
        var selected = Selected();
        return selected switch
        {
            "XunFeiAI" => xunFeiAi,
            "QwenAI"   => qwenAi,
            "OpenAI"   => openAi,
            "GeminiAI" => geminiAi,
            _          => qwenAi
        };
    }
}