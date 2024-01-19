using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Utils;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.k8s.impl;

public class MetricsService(IKubeService kubeService, ILogger<MetricsService> logger) : IMetricsService
{
    public async Task<bool> MetricsServerReady()
    {
        var apis = await kubeService.Client().ListAPIServiceAsync();
        if (apis.Items.Any(x =>
                x.Metadata.Name.EndsWith("metrics.k8s.io") &&
                x.Status.Conditions.Any(y => y.Status == "True" && y.Type == "Available")))
        {
            return true;
        }

        logger.LogInformation("Metrics not ready");
        return false;
    }

    public async Task<IList<NodeMetrics>> NodeMetrics()
    {
        var nodeMetricsList = await kubeService.Client().GetKubernetesNodesMetricsAsync();
        return nodeMetricsList?.Items?.ToList();
    }

    public async Task<IList<PodMetrics>> PodMetrics()
    {
        var podMetricsList = await kubeService.Client().GetKubernetesPodsMetricsAsync();
        return podMetricsList?.Items?.ToList();
    }

    public MetricsQueue<PodMetrics> PodMetricsQueue(string podName)
    {
        var queue = MetricsQueueHelper<PodMetrics>.Instance.Build(podName);
        return queue;
    }
    public MetricsQueue<NodeMetrics> NodeMetricsQueue(string nodeName)
    {
        var queue = MetricsQueueHelper<NodeMetrics>.Instance.Build(nodeName);
        return queue;
    }
}
