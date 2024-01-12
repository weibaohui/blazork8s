using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Chat;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service;

public class HostedService : IHostedService, IDisposable
{
    private readonly ILogger<HostedService> _logger;
    private readonly IKubeService           _baseService;
    private readonly IHubContext<ChatHub>   _ctx;

    public HostedService(ILogger<HostedService> logger, IKubeService baseService, IHubContext<ChatHub> ctx)
    {
        _logger      = logger;
        _baseService = baseService;
        _ctx         = ctx;
    }


    public Task StartAsync(CancellationToken stoppingToken)
    {
        var watchService = ListWatchHelper.Instance.Create(_baseService, _ctx);
        watchService.StartAsync();
        return Task.CompletedTask;
    }


    public Task StopAsync(CancellationToken stoppingToken)
    {
        ListWatchHelper.Instance.Dispose();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        ListWatchHelper.Instance.Dispose();
    }
}
