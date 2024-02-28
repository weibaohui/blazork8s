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

public class HostedService(ILogger<HostedService> logger,
    IKubeService kubeService,
    ClusterInspectionService k8sInspectionService,
    IHubContext<ChatHub> ctx)
    : IHostedService, IDisposable
{
    private readonly ILogger<HostedService> _logger = logger;


    public Task StartAsync(CancellationToken stoppingToken)
    {
        //启动list watch
        var watchService = ListWatchHelper.Instance.Create(kubeService, ctx);
        watchService.StartAsync();

        //启动集群巡检
        k8sInspectionService.StartAsync();

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
