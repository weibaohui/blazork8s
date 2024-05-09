using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorApp.Service.AI.ResponseModels.OpenAI;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.AI.impl;

public class OpenAiService(IConfigService configService, IPromptService promptService, ILogger<OpenAiService> logger)
    : IOpenAiService
{
    public async Task<string> AIChat(string prompt)
    {
        return await Query(prompt);
    }


    public async Task<string> ExplainError(string text)
    {
        var prompt = promptService.GetPrompt("Error");
        var content = $"{prompt} \n {text}";

        return await Query(content);
    }

    public async Task<string> ExplainSecurity(string text)
    {
        var prompt = promptService.GetPrompt("Security");
        var content = $"{prompt} \n {text}";
        return await Query(content);
    }


    public void SetChatEventHandler(EventHandler<string> eventHandler)
    {
        ChatEventHandler = eventHandler;
    }

    public string Name()
    {
        return "OpenAI";
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


    private async Task<string> Query(string promptAndText)
    {
        promptAndText = JsonSerializer.Serialize(promptAndText).TrimStart('"').TrimEnd('"');

        var resp = "";
        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, GetBaseUrl() + "/chat/completions");
        request.Headers.Add("Authorization", "Bearer " + GetToken());
        const string json = """
                            {
                              "model": "${model}",
                              "messages": [
                                 {"role": "user", "content": "${promptAndText}"}
                              ],
                              "temperature": 0.1,
                              "stream": true
                            }
                            """;
        var jsonStr = json.Replace("${promptAndText}", promptAndText)
            .Replace("${model}", GetModel());
        request.Content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
        using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        await using var body = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(body);
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line))
            {
            }
            else if (line.StartsWith("data:"))
            {
                var eventData = line["data:".Length..].Trim();
                if (eventData == "[DONE]") break;

                var completion = JsonSerializer.Deserialize<Response>(eventData);
                var text = completion?.Choices?.FirstOrDefault()?.Delta?.Content;
                resp += text;
                ChatEventHandler?.Invoke(this, text);
            }
            else
            {
                logger.LogInformation("received {Line}", line);
            }
        }

        return resp;
    }
}
