using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorApp.Service.AI.ResponseModels.MoonShot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BlazorApp.Service.AI.impl;

public class MoonShotAiService(IConfigService configService, ILogger<MoonShotAiService> logger) : IMoonShotAiService
{
    public async Task<string> AIChat(string prompt)
    {
        return await Query(prompt);
    }

    public async Task<string> ExplainError(string text)
    {
        var prompt  = configService.GetSection("MoonShotAI")!.GetSection("Prompt").GetValue<string>("error");
        var content = $"{prompt} \n {text}";

        return await Query(content);
    }

    public async Task<string> ExplainSecurity(string text)
    {
        var prompt  = configService.GetSection("MoonShotAI")!.GetSection("Prompt").GetValue<string>("security");
        var content = $"{prompt} \n {text}";
        return await Query(content);
    }


    public void SetChatEventHandler(EventHandler<string> eventHandler)
    {
        ChatEventHandler = eventHandler;
    }

    public string Name()
    {
        return "月之暗面大模型";
    }

    private string GetToken()
    {
        return configService.GetString("MoonShotAI", "Token") ?? string.Empty;
    }

    private string GetBaseUrl()
    {
        return configService.GetString("MoonShotAI", "BaseUrl") ?? string.Empty;
    }

    private string GetModel()
    {
        return configService.GetString("MoonShotAI", "Model") ?? string.Empty;
    }

    private event EventHandler<string> ChatEventHandler;

    private async Task<string> Query(string promptAndText)
    {
        promptAndText = JsonSerializer.Serialize(promptAndText).TrimStart('"').TrimEnd('"');

        var       resp       = "";
        using var httpClient = new HttpClient();
        var       request    = new HttpRequestMessage(HttpMethod.Post, GetBaseUrl() + "/chat/completions");
        request.Headers.Add("Authorization", "Bearer " + GetToken());
        const string json = """
                            {
                              "model": "${model}",
                              "messages": [
                                 {"role": "user", "content": "${promptAndText}"}
                              ],
                              "temperature": 0.3,
                              "stream": true
                            }
                            """;
        var jsonStr = json.Replace("${promptAndText}", promptAndText)
            .Replace("${model}", GetModel());
        request.Content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
        using var       response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        await using var body     = await response.Content.ReadAsStreamAsync();
        using var       reader   = new System.IO.StreamReader(body);

        var isCacheActive = true;
        var buffer        = "";

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
                var text       = completion?.Choices?.FirstOrDefault()?.Delta?.Content;
                resp += text;

                //针对MoonShot 单字出的情况进行缓存处理
                if (isCacheActive)
                {
                    buffer += text;
                    if (buffer.Length <= 30) continue;
                    isCacheActive = false;
                    ChatEventHandler?.Invoke(this, buffer);
                    buffer = "";
                }
                else
                {
                    //非缓存模式，直接输出
                    ChatEventHandler?.Invoke(this, text);
                }
            }
            else
            {
                logger.LogInformation("received {Line}", line);
            }
        }

        return resp;
    }
}
