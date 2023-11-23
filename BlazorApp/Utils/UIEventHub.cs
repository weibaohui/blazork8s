using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class UIEventHub
{
    private static          UIEventHub          _uiEventHub = new UIEventHub();
    private static          HubConnection       _hubConnection;
    private static readonly ILogger<UIEventHub> Logger = LoggingUtils<UIEventHub>.Logger();

    public static UIEventHub Instance()
    {
        return _uiEventHub;
    }

    public async Task<HubConnection> Build(Uri uri)
    {
        if (_hubConnection != null)
        {
            return _hubConnection;
        }

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(uri)
            .Build();
        Logger.LogInformation($"{uri} 连接成功");
        await _hubConnection.StartAsync();

        return _hubConnection;
    }
}