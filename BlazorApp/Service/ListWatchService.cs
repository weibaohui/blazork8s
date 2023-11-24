using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Chat;
using k8s.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service;

public class ListWatchService : IHostedService, IDisposable
{
    private readonly ILogger<ListWatchService> _logger;
    private readonly IBaseService              _baseService;
    private readonly IHubContext<ChatHub>      _ctx;

    public ListWatchService(ILogger<ListWatchService> logger, IBaseService baseService, IHubContext<ChatHub> ctx)
    {
        _logger = logger;
        Console.WriteLine("WatchService 初始化" + DateTime.Now);
        _baseService = baseService;
        _ctx         = ctx;
    }


    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

#pragma warning disable CS4014
        WatchAllPod();
        WatchAllDeployment();
        WatchAllReplicaSet();
        WatchAllNode();
#pragma warning restore CS4014
        return Task.CompletedTask;
    }


    private async Task WatchAllPod()
    {
        var listResp = _baseService.Client().CoreV1
            .ListPodForAllNamespacesWithHttpMessagesAsync(watch: true);
        await new Watcher<V1Pod, V1PodList>(_ctx).Watch(listResp);
    }

    private async Task WatchAllDeployment()
    {
        var listResp = _baseService.Client().AppsV1.ListDeploymentForAllNamespacesWithHttpMessagesAsync(watch: true);
        await new Watcher<V1Deployment, V1DeploymentList>(_ctx).Watch(listResp);
    }

    private async Task WatchAllReplicaSet()
    {
        var listResp = _baseService.Client().AppsV1.ListReplicaSetForAllNamespacesWithHttpMessagesAsync(watch: true);
        await new Watcher<V1ReplicaSet, V1ReplicaSetList>(_ctx).Watch(listResp);
    }

    private async Task WatchAllNode()
    {
        var listResp = _baseService.Client().CoreV1.ListNodeWithHttpMessagesAsync(watch: true);
        await new Watcher<V1Node, V1NodeList>(_ctx).Watch(listResp);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
    }
}
