#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Chat;
using Entity;
using Extension;
using FluentScheduler;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp.Utils;

public class PortForwardExecutorHelper
{
    private static readonly ILogger<PortForwardExecutorHelper> Logger =
        LoggingHelper<PortForwardExecutorHelper>.Logger();

    private static readonly Dictionary<string, PortForwardExecutor> Map = new();
    private readonly        ResourceCache<PortForward> _cache = ResourceCacheHelper<PortForward>.Instance.Build();
    private readonly        object _lockObj = new object();
    private                 IHubContext<ChatHub> _ctx;
    public static           PortForwardExecutorHelper Instance => Nested.Instance;

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly PortForwardExecutorHelper Instance = new PortForwardExecutorHelper();
    }

    public PortForwardExecutorHelper()
    {
        //每5秒执行一次端口探活
        JobManager.Initialize();
        JobManager.AddJob(NcProbe, (s) => s.ToRunEvery(5).Seconds());
        JobManager.AddJob(RemoveFailedPort, (s) => s.ToRunEvery(30).Seconds());
    }


    /// <summary>
    /// 探测端口是否存活
    /// </summary>
    private async void NcProbe()
    {
        Logger.LogInformation("开始探测{Count}端口是否存活", Map.Count);
        if (Map.Count == 0)
        {
            return;
        }

        lock (_lockObj)
        {
            foreach (var (_, pfe) in Map)
            {
                var pf = pfe.PortForward;
                if (pf.StatusTimestamp != null && pf.StatusTimestamp.FromNowSeconds() < 10)
                {
                    continue;
                }

                pfe.StartProbe();
                WatchUpdate(WatchEventType.Modified, pf);
                Logger.LogInformation(
                    "探测状态:{Port} {Status},{Time}",
                    pf.LocalPort,
                    pf.Status,
                    pf.StatusTimestamp);
            }
        }
    }

    private async void RemoveFailedPort()
    {
        Logger.LogInformation("清除失败端口");
        if (Map.Count == 0)
        {
            return;
        }

        lock (_lockObj)
        {
            foreach (var (_, pfe) in Map)
            {
                var pf = pfe.PortForward;
                if (pf.StatusTimestamp != null && pf.Status.IsNullOrEmpty())
                {
                    DisposeByItem(pf);
                }

                if (pf.Status == "failed")
                {
                    DisposeByItem(pf);
                }
            }
        }
    }

    public async Task ForwardPort(PortForwardType type, string ns, string kubeName, string kubePort, int localPort)
    {
        var pf = new PortForward
        {
            Kind       = "PortForward",
            ApiVersion = "blazorK8s.io/v1",
            Type       = type,
            LocalPort  = localPort,
            KubePort   = kubePort,
            KubeName   = kubeName,
            Metadata = new V1ObjectMeta
            {
                Name              = $"{kubeName}-{kubePort}-{localPort}",
                NamespaceProperty = ns,
                //k8s时间
                CreationTimestamp = DateTime.Now.ToUniversalTime(),
                Uid               = Guid.NewGuid().ToString()
            }
        };

        PortForwardExecutor pfe = new PortForwardExecutor(pf);
        lock (_lockObj)
        {
            Map.TryAdd(pfe.PortForward.Metadata.Name, pfe);
        }

        await pfe.Start();
       await WatchUpdate(WatchEventType.Added, pf);

    }


    /// <summary>
    /// 关闭端口转发终端，释放资源
    /// </summary>
    /// <param name="pf">PortForward</param>
    public void DisposeByItem(PortForward pf)
    {
        string name = pf.Metadata.Name;
        lock (_lockObj)
        {
            Map.TryGetValue(name, out PortForwardExecutor? executor);
            if (executor != null)
            {
                executor.Dispose();
                lock (_lockObj)
                {
                    Map.Remove(name);
                }

                WatchUpdate(WatchEventType.Deleted, executor.PortForward);
            }
        }

        Logger.LogInformation("清除失败端口{Port}", pf.LocalPort);
    }


    /// <summary>
    /// 通过PortForwardService 注入 IHubContext
    /// 通过IHubContext 发送 即时消息
    /// </summary>
    /// <param name="ctx"></param>
    public void SetIHubContext(IHubContext<ChatHub> ctx)
    {
        _ctx = ctx;
    }


    public async Task WatchUpdate(WatchEventType type,PortForward item)
    {
        if (_ctx == null)
        {
            return;
        }
        var data = new ResourceWatchEntity<PortForward>
        {
            Message = $"{type}:{item.Kind}:{item.Metadata.Name}",
            Type    = type,
            Item    = item
        };
        Logger.LogInformation("{Type}:{ItemKind}:{MetadataName}", type, item.Kind, item.Metadata.Name);
        _cache.Update(type, item);
        await _ctx.Clients.All.SendAsync("ReceiveMessage", data);
    }
}
