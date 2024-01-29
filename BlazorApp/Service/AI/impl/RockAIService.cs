using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Newtonsoft.Json;

namespace BlazorApp.Service.AI.impl;

public class RockAiService : IRockAiService
{
    private readonly IConfigService _configService;

    public RockAiService(IConfigService configService)
    {
        _configService = configService;
    }


    public async Task<string> Chat(string txtValue)
    {
        var url   = _configService.GetString("RockAI", "Url");
        var appid = _configService.GetString("RockAI", "AppId");
        var sid   = _configService.GetString("RockAI", "Sid");
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
}
