using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlazorApp.Service.AI.impl;

public class RockAiService(IConfigService configService, ILogger<RockAiService> logger) : IRockAiService
{

    public async Task<string> AIChat(string prompt)
    {
        return await Query(prompt);
    }

    public async Task<string> ExplainError(string text)
    {
        var prompt  = configService.GetSection("RockAI")!.GetSection("Prompt").GetValue<string>("error");
        var content = $"{prompt} \n {text}";
        return await AIChat(content);    }

    public async Task<string> ExplainSecurity(string text)
    {
        var prompt  = configService.GetSection("RockAI")!.GetSection("Prompt").GetValue<string>("security");
        var content = $"{prompt} \n {text}";
        return await AIChat(content);
    }

    private async Task<string> Query(string txtValue)
    {
        var url   = configService.GetString("RockAI", "Url");
        var appid = configService.GetString("RockAI", "AppId");
        var sid   = configService.GetString("RockAI", "Sid");
        Console.WriteLine(url);
        var chat = new RockAI.Chat()
        {
            appid = appid,
            sid   = sid,
            text  = txtValue,
        };
        var handler = new HttpClientHandler();
        handler.ClientCertificateOptions                  = ClientCertificateOption.Automatic;
        handler.SslProtocols                              = SslProtocols.None;
        handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true;

        using var client = new HttpClient(handler);


        var response = await client.PostAsync(url,
            new StringContent(JsonConvert.SerializeObject(chat), Encoding.UTF8, "application/json"));
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var todo    = JsonConvert.DeserializeObject<RockAI.Root>(content);
            // Console.WriteLine($"Todo ID: {todo.Id}");
            Console.WriteLine($"{todo.answer.text}");
            return todo.answer.text;
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            return "";
        }
    }

    public void SetChatEventHandler(EventHandler<string> handler)
    {
        logger.LogInformation("SetChatEventHandler");
    }
    public string Name()
    {
        return "自研大模型";
    }
}
