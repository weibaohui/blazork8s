using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Utils;
using FluentScheduler;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.k8s;

public class MetricsQueueWatchService : IHostedService, IDisposable
{
    private readonly ILogger<MetricsQueueWatchService> _logger;

    private readonly IKubeService _kubeService;

    public MetricsQueueWatchService(IKubeService kubeService, ILogger<MetricsQueueWatchService> logger)
    {
        _kubeService = kubeService;
        _logger      = logger;
    }


    private async void Watch()
    {
        // _logger.LogInformation("MetricsQueueWatchService working");
        var podMetricsList = await _kubeService.Client().GetKubernetesPodsMetricsAsync();

        foreach (var metrics in podMetricsList.Items)
        {
            var queue = MetricsQueueHelper<PodMetrics>.Instance.Build(metrics.Name());
            queue.Enqueue(metrics);
        }

        var nodeMetricsList = await _kubeService.Client().GetKubernetesNodesMetricsAsync();
        foreach (var metrics in nodeMetricsList.Items)
        {
            var queue = MetricsQueueHelper<NodeMetrics>.Instance.Build(metrics.Name());
            queue.Enqueue(metrics);
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
//每5秒执行一次指标更新
        JobManager.Initialize();
        JobManager.AddJob(Watch, (s) => s.ToRunEvery(1).Seconds());
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
    }
}
