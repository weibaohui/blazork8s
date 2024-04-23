using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace BlazorApp.Service.AI.impl;

public class PromptService(IConfigService configService, IStringLocalizer L) : IPromptService
{
    public string GetPrompt(string key)
    {
        var prompt = configService.GetSection("Prompt")?.GetValue<string>(key);
        if (prompt != null && prompt.Contains("{language}"))
        {
            prompt = prompt.Replace("{language}", GetLanguage());
        }

        return prompt;
    }


    private string GetLanguage()
    {
        var x = (SimpleI18NStringLocalizer)L;
        var cultureName = x.GetCulture().Name;
        var language = SimpleI18NStringLocalizer.LanguageMap[cultureName];
        return language;
    }
}
