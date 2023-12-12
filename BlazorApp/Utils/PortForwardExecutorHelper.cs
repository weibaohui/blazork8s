#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using FluentScheduler;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class PortForwardExecutorHelper
{
    private static readonly ILogger<PortForwardExecutorHelper> Logger =
        LoggingHelper<PortForwardExecutorHelper>.Logger();

    private static readonly Dictionary<string, PortForwardExecutor> Map = new();
    private                 ResourceCache<PortForward> cache = ResourceCacheHelper<PortForward>.Instance.Build();

    public static PortForwardExecutorHelper Instance => Nested.Instance;

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
    }


    /// <summary>
    /// 探测端口是否存活
    /// </summary>
    private async void NcProbe()
    {
        Logger.LogInformation("开始探测");
        if (Map.Count == 0)
        {
            return;
        }

        foreach (var (_, pfe) in Map)
        {
            var pf = pfe.PortForward;
            Logger.LogInformation(
                "探测状态:{Port} {Status},{Time}",
                pf.LocalPort,
                pf.Status,
                pf.StatusTimestamp);
            await pfe.StartProbe();
            cache.Update(WatchEventType.Modified, pf);

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
            KubeName = kubeName,
            Metadata = new V1ObjectMeta
            {
                Name              = $"{kubeName}-{kubePort}-{localPort}",
                NamespaceProperty = ns,
                //k8s时间
                CreationTimestamp = DateTime.Now.ToUniversalTime(),
                Uid = Guid.NewGuid().ToString()
            }
        };

        PortForwardExecutor pfe = new PortForwardExecutor(pf);
        Map.TryAdd(pfe.Command(), pfe);
        cache.Update(WatchEventType.Added, pf);
        await pfe.Start();
    }
}
