using System;
using System.Threading.Tasks;
using BlazorApp.Chat;
using BlazorApp.Utils;
using Entity;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Service.impl;

public class WatchService : IWatchService
{
    private readonly IBaseService _baseService;

    private readonly IHubContext<ChatHub> _ctx;

    public WatchService(IBaseService baseService, IHubContext<ChatHub> ctx)
    {
        Console.WriteLine("WatchService 初始化" + DateTime.Now);
        _baseService = baseService;
        _ctx         = ctx;
        WatchAllPod();
        WatchAllDeployment();
    }

    public async Task WatchAllPod()
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

    public async Task WatchAllDeployment()
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
}
