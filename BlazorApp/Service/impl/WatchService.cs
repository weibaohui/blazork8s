using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Chat;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace BlazorApp.Service.impl;

public class WatchService : IWatchService
{
    private readonly IBaseService _baseService;

    private readonly List<V1Pod>          _sharedPods = new();
    private          bool                 _podListChangedByWatch;
    private readonly IHubContext<ChatHub> _ctx;


    public WatchService(IBaseService baseService, IHubContext<ChatHub> ctx)
    {
        Console.WriteLine("WatchService 初始化" + DateTime.Now);
        _baseService = baseService;
        _ctx         = ctx;
        WatchAllPod();
    }


    public void UpdateSharePods(WatchEventType type, V1Pod item)
    {
        var index = _sharedPods.FindIndex(0, r => r.Uid() == item.Uid());
        // Console.WriteLine($"{index}:{item.Name()}");
        switch (type)
        {
            case WatchEventType.Added:
                if (index == -1)
                {
                    //不存在
                    _sharedPods.Insert(0, item);
                    _podListChangedByWatch = true;
                }

                break;
            case WatchEventType.Modified:
                if (index > -1)
                {
                    //已存在
                    _sharedPods[index]     = item;
                    _podListChangedByWatch = true;
                }

                break;
            case WatchEventType.Deleted:
                if (index > -1)
                {
                    //已存在
                    _sharedPods.RemoveAt(index);
                    _podListChangedByWatch = true;
                }

                break;
            case WatchEventType.Error:
                break;
            case WatchEventType.Bookmark:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }


    public async Task WatchAllPod()
    {
        var podListResp = _baseService.Client().CoreV1
            .ListPodForAllNamespacesWithHttpMessagesAsync(watch: true);
        await foreach (var (type, item) in podListResp.WatchAsync<V1Pod, V1PodList>())
        {
            Console.WriteLine("==on watch event start ==");
            Console.WriteLine($"{type}:{item.Metadata.Name}");
            // UpdateSharePods(type, item);
            Console.WriteLine("==on watch event end ==");
            Console.WriteLine("==on SendAsync event start ==");
            await _ctx.Clients.All.SendAsync("ReceiveMessage", $"{type}:{item.Metadata.Name}");
            Console.WriteLine("==on SendAsync event end ==");
        }
    }

    public bool PodListChangedByWatch()
    {
        return _podListChangedByWatch;
    }

    public async Task<IList<V1Pod>> ListPods()
    {
        if (_sharedPods.Count == 0)
        {
            var pods = await _baseService.Client().ListPodForAllNamespacesAsync();
            foreach (var item in pods.Items)
            {
                UpdateSharePods(WatchEventType.Added, item);
            }
        }

        _podListChangedByWatch = false;
        return _sharedPods;
    }
}
