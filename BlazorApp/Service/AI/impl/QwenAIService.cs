using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorApp.Service.AI.ResponseModels.Qwen;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.AI.impl;

public class QwenAiService(IConfigService configService, IPromptService promptService, ILogger<QwenAiService> logger)
    : IQwenAiService
{
    private const string SseEndpoint = "https://dashscope.aliyuncs.com/api/v1/services/aigc/text-generation/generation";
    private string _incrementResp = "";
    private string _lastResp = "";

    public async Task<string> AIChat(string prompt)
    {
        return await Query(prompt);
    }

    public void SetChatEventHandler(EventHandler<string> eventHandler)
    {
        ChatEventHandler = eventHandler;
    }

    public async Task<string> ExplainError(string text)
    {
        var prompt = promptService.GetPrompt("error");
        var content = $"{prompt} \n {text}";
        return await Query(content);
    }

    public async Task<string> ExplainSecurity(string text)
    {
        var prompt = promptService.GetPrompt("security");
        var content = $"{prompt} \n {text}";
        return await Query(content);
    }

    public string Name()
    {
        return "阿里通义千问大模型";
    }

    private string GetModel()
    {
        return configService.GetString("QwenAI", "Model") ?? string.Empty;
    }

    private event EventHandler<string> ChatEventHandler;


    private string GetApiKey()
    {
        return configService.GetString("QwenAI", "APIKey") ?? string.Empty;
    }

    private async Task<string> Query(string promptAndText)
    {
        promptAndText = JsonSerializer.Serialize(promptAndText).TrimStart('"').TrimEnd('"');

        var resp = "";
        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, SseEndpoint);
        request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/event-stream"));
        request.Headers.Add("Authorization", "Bearer " + GetApiKey());
        const string json = """
                            {
                                "model": "${model}",
                                "input": {
                                    "prompt": "${promptAndText}"
                                },
                                "parameters": {
                                }
                            }
                            """;
        var jsonStr = json.Replace("${promptAndText}", promptAndText)
            .Replace("${model}", GetModel());
        request.Content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
        using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        await using var body = await response.Content.ReadAsStreamAsync();
        using var reader = new System.IO.StreamReader(body);
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line))
            {
            }
            else if (line.StartsWith("data:"))
            {
                var eventData = line["data:".Length..].Trim();
                var chatResponse = JsonSerializer.Deserialize<Response>(eventData);
                var text = chatResponse?.Output?.Text;
                if (chatResponse?.Output?.FinishReason == "stop")
                {
                    //阿里通义千问最后一次返回全部
                    resp = text;
                }

                if (string.IsNullOrEmpty(text))
                {
                    continue;
                }

                IncrementHandler(text);
            }
            else
            {
                logger.LogInformation("received {Line}", line);
            }
        }

        return resp;
    }

    private void IncrementHandler(string resp)
    {
        if (string.IsNullOrWhiteSpace(_lastResp))
        {
            _incrementResp = resp;
        }
        else
        {
            _incrementResp = resp.Replace(_lastResp, "");
        }

        _lastResp = resp;
        // logger.LogInformation("{Ai} _incrementResp: {IncrementResp}", Name(), _incrementResp);
        ChatEventHandler?.Invoke(this, _incrementResp);
    }
}
