using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazorApp.Service.AI.impl;

public class XunFeiAiService(IConfigService configService) : IXunFeiAiService
{
    private static ClientWebSocket _webSocket;

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
        var prompt  = configService.GetSection("XunFeiAI")!.GetSection("Prompt").GetValue<string>("error");
        var content = $"{prompt} \n {text}";
        return await Query(content);
    }

    public async Task<string> ExplainSecurity(string text)
    {
        var prompt  = configService.GetSection("XunFeiAI")!.GetSection("Prompt").GetValue<string>("security");
        var content = $"{prompt} \n {text}";
        return await Query(content);
    }

    public string Name()
    {
        return "科大讯飞星火大模型";
    }

    private event EventHandler<string> ChatEventHandler;

    private string GetHostUrl()
    {
        return $"https://spark-api.xf-yun.com/{GetVersion()}/chat";
    }

    private string GetAppId()
    {
        return configService.GetString("XunFeiAI", "APPID") ?? string.Empty;
    }

    private string GetApiSecret()
    {
        return configService.GetString("XunFeiAI", "APISecret") ?? string.Empty;
    }

    private string GetApiKey()
    {
        return configService.GetString("XunFeiAI", "APIKey") ?? string.Empty;
    }

    private string GetVersion()
    {
        return configService.GetString("XunFeiAI", "Version") ?? string.Empty;
    }

    private string GetDomain()
    {
        return configService.GetString("XunFeiAI", "Domain") ?? string.Empty;
    }

    private async Task<string> Query(string promptAndText)
    {
        var resp    = "";
        var authUrl = GetAuthUrl();
        var url     = authUrl.Replace("http://", "ws://").Replace("https://", "wss://");
        using (_webSocket = new ClientWebSocket())
        {
            await _webSocket.ConnectAsync(new Uri(url), CancellationToken.None);

            var request = new IXunFeiAiService.JsonRequest
            {
                header = new IXunFeiAiService.Header()
                {
                    app_id = GetAppId(),
                    uid    = "12345"
                },
                parameter = new IXunFeiAiService.Parameter()
                {
                    chat = new IXunFeiAiService.Chat()
                    {
                        domain           = GetDomain(), //模型领域，默认为星火通用大模型
                        temperature      = 0.5,         //温度采样阈值，用于控制生成内容的随机性和多样性，值越大多样性越高；范围（0，1）
                        max_tokens       = 1024,        //生成内容的最大长度，范围（0，4096）
                        auditing         = "default",
                        random_threshold = 0.5,
                    }
                },
                payload = new IXunFeiAiService.Payload()
                {
                    message = new IXunFeiAiService.Message()
                    {
                        text = new List<IXunFeiAiService.Content>
                        {
                            new() { role = "user", content = promptAndText },
                            // new Content() { role = "assistant", content = "....." }, // AI的历史回答结果，这里省略了具体内容，可以根据需要添加更多历史对话信息和最新问题的内容。
                        }
                    }
                }
            };

            string jsonString = JsonConvert.SerializeObject(request);
            //连接成功，开始发送数据


            var frameData2 = Encoding.UTF8.GetBytes(jsonString.ToString());


            await _webSocket.SendAsync(new ArraySegment<byte>(frameData2), WebSocketMessageType.Text, true,
                CancellationToken.None);

            // 接收流式返回结果进行解析
            var receiveBuffer = new byte[1024];
            var result =
                await _webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);
                    //将结果构造为json

                    JObject jsonObj = JObject.Parse(receivedMessage);
                    int     code    = (int)jsonObj["header"]?["code"];


                    if (0 == code)
                    {
                        var status = (int)jsonObj["payload"]?["choices"]?["status"];


                        JArray textArray = (JArray)jsonObj["payload"]?["choices"]?["text"];
                        var    content   = (string)textArray?[0]["content"];
                        resp += content;

                        ChatEventHandler?.Invoke(this, content);
                        if (status != 2)
                        {
                            // logger.LogInformation("已接收到数据： {ReceivedMessage}", receivedMessage);
                        }
                        else
                        {
                            // logger.LogInformation("最后一帧： {ReceivedMessage}", receivedMessage);
                            // var totalTokens = (int)jsonObj["payload"]?["usage"]?["text"]?["total_tokens"];
                            // logger.LogInformation("整体返回结果： {Resp}", resp);
                            // logger.LogInformation("本次消耗token数： {TotalTokens}", totalTokens);
                            break;
                        }
                    }
                    else
                    {
                        // logger.LogInformation("请求报错： {ReceivedMessage}", receivedMessage);
                    }
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    // logger.LogInformation("已关闭WebSocket连接");
                    break;
                }

                result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
            }
        }


        return resp;
    }

    /// <summary>
    /// 返回code为错误码时，请查询https://www.xfyun.cn/document/error-code解决方案
    /// </summary>
    /// <returns></returns>
    string GetAuthUrl()
    {
        var date = DateTime.UtcNow.ToString("r");

        var uri = new Uri(GetHostUrl());
        var builder = new StringBuilder("host: ").Append(uri.Host).Append("\n")
            .Append("date: ").Append(date).Append("\n")
            .Append("GET ").Append(uri.LocalPath).Append(" HTTP/1.1");

        var sha = HmaCsha256(GetApiSecret(), builder.ToString());
        var authorization =
            $"api_key=\"{GetApiKey()}\", algorithm=\"hmac-sha256\", headers=\"host date request-line\", signature=\"{sha}\"";

        var newUrl = "https://" + uri.Host + uri.LocalPath;

        var path1 = "authorization" + "=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(authorization));
        date = date.Replace(" ", "%20").Replace(":", "%3A").Replace(",", "%2C");
        var path2 = "date" + "=" + date;
        var path3 = "host" + "=" + uri.Host;

        newUrl = newUrl + "?" + path1 + "&" + path2 + "&" + path3;
        return newUrl;
    }


    string HmaCsha256(string apiSecretIsKey, string buider)
    {
        var bytes      = Encoding.UTF8.GetBytes(apiSecretIsKey);
        var hMACSHA256 = new HMACSHA256(bytes);
        var date       = Encoding.UTF8.GetBytes(buider);
        date = hMACSHA256.ComputeHash(date);
        hMACSHA256.Clear();
        return Convert.ToBase64String(date);
    }
}
