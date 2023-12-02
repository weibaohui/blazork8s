using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Chat;
using k8s.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.k8s;

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
        WatchPod();
        WatchDeployment();
        WatchReplicaSet();
        WatchNode();
        WatchEvent();
        WatchNamespace();
        WatchDaemonSet();
#pragma warning restore CS4014
        return Task.CompletedTask;
    }


    private async Task WatchPod()
    {
        var listResp = _baseService.Client().CoreV1
            .ListPodForAllNamespacesWithHttpMessagesAsync(watch: true);
        await new Watcher<V1Pod, V1PodList>(_ctx).Watch(listResp);
    }
    private async Task WatchDaemonSet()
    {
        var listResp = _baseService.Client().AppsV1
            .ListDaemonSetForAllNamespacesWithHttpMessagesAsync(watch: true);
        await new Watcher<V1DaemonSet, V1DaemonSetList>(_ctx).Watch(listResp);
    }

    private async Task WatchEvent()
    {
        var listResp = _baseService.Client().CoreV1
            .ListEventForAllNamespacesWithHttpMessagesAsync(watch: true);
        await new Watcher<Corev1Event, Corev1EventList>(_ctx).Watch(listResp);
    }

    private async Task WatchDeployment()
    {
        var listResp = _baseService.Client().AppsV1.ListDeploymentForAllNamespacesWithHttpMessagesAsync(watch: true);
        await new Watcher<V1Deployment, V1DeploymentList>(_ctx).Watch(listResp);
    }

    private async Task WatchReplicaSet()
    {
        var listResp = _baseService.Client().AppsV1.ListReplicaSetForAllNamespacesWithHttpMessagesAsync(watch: true);
        await new Watcher<V1ReplicaSet, V1ReplicaSetList>(_ctx).Watch(listResp);
    }

    private async Task WatchNode()
    {
        var listResp = _baseService.Client().CoreV1.ListNodeWithHttpMessagesAsync(watch: true);
        await new Watcher<V1Node, V1NodeList>(_ctx).Watch(listResp);
    }

    private async Task WatchNamespace()
    {
        var listResp = _baseService.Client().CoreV1.ListNamespaceWithHttpMessagesAsync(watch: true);
        await new Watcher<V1Namespace, V1NamespaceList>(_ctx).Watch(listResp);
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
