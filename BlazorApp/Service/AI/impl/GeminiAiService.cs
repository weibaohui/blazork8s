using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlazorApp.Service.AI.impl;

public class GeminiAiService(
    IConfigService configService,
    IPromptService promptService,
    ILogger<GeminiAiService> logger) : IGeminiAiService
{
    public async Task<string> AIChat(string prompt)
    {
        prompt = JsonConvert.SerializeObject(prompt).TrimStart('"').TrimEnd('"');
        return await Query(prompt);
    }

    public void SetChatEventHandler(EventHandler<string> eventHandler)
    {
        ChatEventHandler = eventHandler;
    }

    public async Task<string> ExplainError(string text)
    {
        var prompt = promptService.GetPrompt("error");
        text = JsonConvert.SerializeObject(text).TrimStart('"').TrimEnd('"');
        var content = $"{prompt}:{text}";
        return await Query(content);
    }

    public async Task<string> ExplainSecurity(string text)
    {
        var prompt = promptService.GetPrompt("security");
        text = JsonConvert.SerializeObject(text).TrimStart('"').TrimEnd('"');
        var content = $"{prompt}:{text}";
        return await Query(content);
    }

    public string Name()
    {
        return "Google Gemini";
    }

    private event EventHandler<string> ChatEventHandler;

    private string GetSseEndpoint()
    {
        return
            $"https://generativelanguage.googleapis.com/v1beta/models/{GetModel()}:streamGenerateContent?key={GetApiKey()}";
    }

    private string GetModel()
    {
        return configService.GetString("GeminiAI", "Model") ?? string.Empty;
    }


    private string GetApiKey()
    {
        return configService.GetString("GeminiAI", "APIKey") ?? string.Empty;
    }

    private async Task<string> Query(string promptAndText)
    {
        // Console.WriteLine($"Gemini {GetSseEndpoint()} ");
        // Console.WriteLine($"Gemini {promptAndText} ");
        var resp = "";
        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, GetSseEndpoint());
        var json = """
                   {
                       "contents": [
                           {
                               "parts": [
                                   {
                                       "text": "${promptAndText}"
                                   }
                               ]
                           }
                       ]
                   }
                   """;
        request.Headers.Add("Cache-Control", "no-cache");
        request.Content = new StringContent(json.Replace("${promptAndText}", promptAndText), Encoding.UTF8,
            "application/json");
        try
        {
            using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            await using var body = await response.Content.ReadAsStreamAsync();
            using var reader = new System.IO.StreamReader(body);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue;
                line = line.Trim();
                if (line.StartsWith("\"text\":"))
                {
                    var data = line.Split("\"text\":")[1].Trim();
                    data = data.Trim('"');
                    resp += data;
                    ChatEventHandler?.Invoke(this, data);
                }

                logger.LogInformation("received  {Line}", line);
            }
        }
        catch (Exception e)
        {
            resp = "Error" + e.Message;
            logger.LogError(e, "Error");
        }

        return resp;
    }
}
