using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.AI.impl;

public class PromptService(IConfigService configService, ILogger<PromptService> logger) : IPromptService
{
    [Inject] public IStringLocalizer L { get; set; }

    public string GetPrompt(string key)
    {
        var prompt = configService.GetSection("Prompt")?.GetValue<string>(key);
        return prompt;
    }
}
