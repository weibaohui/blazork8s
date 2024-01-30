using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazorApp.Service.AI.impl;

public class QwenAiService(IConfigService configService, ILogger<QwenAiService> logger) : IQwenAiService
{
    string sseEndpoint =
        "https://dashscope.aliyuncs.com/api/v1/services/aigc/text-generation/generation";

    private event EventHandler<string> ChatEventHandler;


    private string GetApiKey()
    {
        return configService.GetString("QwenAI", "APIKey") ?? string.Empty;
    }

    public async Task<string> AIChat(string prompt)
    {
        prompt = JsonConvert.SerializeObject(prompt).TrimStart('"').TrimEnd('"');
        return await Query(prompt);
    }

    public void SetChatEventHandler(EventHandler<string> eventHandler)
    {
        ChatEventHandler += eventHandler;
    }

    private async Task<string> Query(string promptAndText)
    {
        var       resp       = "";
        using var httpClient = new HttpClient();
        var       request    = new HttpRequestMessage(HttpMethod.Post, sseEndpoint);
        request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/event-stream"));
        request.Headers.Add("Authorization", "Bearer " + GetApiKey());
        var json = """
                   {
                       "model": "qwen-1.8b-chat",
                       "input": {
                           "prompt": "${promptAndText}"
                       },
                       "parameters": {
                       }
                   }
                   """;
        request.Content = new StringContent(json.Replace("${promptAndText}", promptAndText), Encoding.UTF8,
            "application/json");
        using var       response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        await using var body     = await response.Content.ReadAsStreamAsync();
        using var       reader   = new System.IO.StreamReader(body);
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line))
            {
            }
            else if (line.StartsWith("data:"))
            {
                var     eventData    = line["data:".Length..].Trim();
                JObject jsonObj      = JObject.Parse(eventData);
                var     finishReason = (string)jsonObj["output"]?["finish_reason"];
                var     text         = (string)jsonObj["output"]?["text"];
                if (finishReason == "stop")
                {
                    resp = text;
                }

                if (text.IsNullOrEmpty())
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


    private string _incrementResp = "";
    private string _lastResp      = "";

    private void IncrementHandler(string resp)
    {
        if (!string.IsNullOrWhiteSpace(_lastResp))
        {
            _incrementResp = resp.Replace(_lastResp, "");
        }
        else
        {
            _incrementResp = resp;
        }

        _lastResp = resp;
        logger.LogInformation("{Ai} _incrementResp: {IncrementResp}", Name(), _incrementResp);
        if (ChatEventHandler != null) ChatEventHandler(this, _incrementResp);
    }

    public async Task<string> ExplainError(string text)
    {

        var prompt  = configService.GetSection("QwenAI")!.GetSection("Prompt").GetValue<string>("error");
        text = JsonConvert.SerializeObject(text).TrimStart('"').TrimEnd('"');
        var content = $"{prompt}:{text}";
        return await Query(content);
    }

    public async Task<string> ExplainSecurity(string text)
    {
        var prompt  = configService.GetSection("QwenAI")!.GetSection("Prompt").GetValue<string>("security");
        text = JsonConvert.SerializeObject(text).TrimStart('"').TrimEnd('"');
        var content = $"{prompt}:{text}";
        return await Query(content);
    }

    public string Name()
    {
        return "阿里通义千问大模型";
    }
}
