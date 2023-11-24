using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Chat;
using BlazorApp.Utils;
using Entity;
using k8s;
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

        WatchAllPod();
        WatchAllDeployment();
        WatchAllReplicaSet();
        return Task.CompletedTask;
    }


    private async Task WatchAllPod()
    {
        var cache = ResourceCacheHelper<V1Pod>.Instance.Build();
        var podListResp = _baseService.Client().CoreV1
            .ListPodForAllNamespacesWithHttpMessagesAsync(watch: true);

        await foreach (var (type, item) in podListResp.WatchAsync<V1Pod, V1PodList>())
        {
            var data = new ResourceWatchEntity<V1Pod>
            {
                Message = $"{type}:{item.Kind}:{item.Metadata.Name}",
                Type    = type,
                Item    = item
            };
            Console.WriteLine($"{type}:{item.Kind}:{item.Metadata.Name}");
            cache.Update(type, item);
            await _ctx.Clients.All.SendAsync("ReceiveMessage", data);
        }
    }

    private async Task WatchAllDeployment()
    {
        var cache    = ResourceCacheHelper<V1Deployment>.Instance.Build();
        var listResp = _baseService.Client().AppsV1.ListDeploymentForAllNamespacesWithHttpMessagesAsync(watch: true);
        await foreach (var (type, item) in listResp.WatchAsync<V1Deployment, V1DeploymentList>())
        {
            var data = new ResourceWatchEntity<V1Deployment>
            {
                Message = $"{type}:{item.Kind}:{item.Metadata.Name}",
                Type    = type,
                Item    = item
            };
            Console.WriteLine($"{type}:{item.Kind}:{item.Metadata.Name}");
            cache.Update(type, item);
            await _ctx.Clients.All.SendAsync("ReceiveMessage", data);
        }
    }

    private async Task WatchAllReplicaSet()
    {
        var cache    = ResourceCacheHelper<V1ReplicaSet>.Instance.Build();
        var listResp = _baseService.Client().AppsV1.ListReplicaSetForAllNamespacesWithHttpMessagesAsync(watch: true);
        await foreach (var (type, item) in listResp.WatchAsync<V1ReplicaSet, V1ReplicaSetList>())
        {
            var data = new ResourceWatchEntity<V1ReplicaSet>
            {
                Message = $"{type}:{item.Kind}:{item.Metadata.Name}",
                Type    = type,
                Item    = item
            };
            Console.WriteLine($"{type}:{item.Kind}:{item.Metadata.Name}");
            cache.Update(type, item);
            await _ctx.Clients.All.SendAsync("ReceiveMessage", data);
        }
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
