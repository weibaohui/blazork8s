using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Utils;
using FluentScheduler;
using k8s.Models;
using Microsoft.Extensions.Hosting;

namespace BlazorApp.Service.k8s;

public class MetricsQueueWatchService(
    IMetricsService metricsService)
    : IHostedService, IDisposable
{
    public void Dispose()
    {
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        //每1秒执行一次指标更新
        JobManager.Initialize();
        JobManager.AddJob(Watch, (s) => s.ToRunEvery(1).Seconds());
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async void Watch()
    {
        var ready = await metricsService.MetricsServerReady();
        if (!ready)
        {
            // _logger.LogInformation("Metrics not ready");
            return;
        }


        foreach (var metrics in await metricsService.PodMetrics())
        {
            var queue = MetricsQueueHelper<PodMetrics>.Instance.Build(metrics.Name());
            queue.Enqueue(metrics);
        }

        foreach (var metrics in await metricsService.NodeMetrics())
        {
            var queue = MetricsQueueHelper<NodeMetrics>.Instance.Build(metrics.Name());
            queue.Enqueue(metrics);
        }
    }
}
