using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Utils;
using FluentScheduler;
using k8s.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.k8s;

public class MetricsQueueWatchService : IHostedService, IDisposable
{
    private readonly ILogger<MetricsQueueWatchService> _logger;

    private readonly IKubeService    _kubeService;
    private readonly IMetricsService _metricsService;


    public MetricsQueueWatchService(IKubeService kubeService, ILogger<MetricsQueueWatchService> logger,
        IMetricsService                          metricsService)
    {
        _kubeService    = kubeService;
        _logger         = logger;
        _metricsService = metricsService;
    }


    private async void Watch()
    {
        var ready = await _metricsService.MetricsServerReady();
        if (!ready)
        {
            _logger.LogInformation("Metrics not ready");
            return;
        }


        foreach (var metrics in await _metricsService.PodMetrics())
        {
            var queue = MetricsQueueHelper<PodMetrics>.Instance.Build(metrics.Name());
            queue.Enqueue(metrics);
        }

        foreach (var metrics in await _metricsService.NodeMetrics())
        {
            var queue = MetricsQueueHelper<NodeMetrics>.Instance.Build(metrics.Name());
            queue.Enqueue(metrics);
        }
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

public void Dispose()
{
}

}
