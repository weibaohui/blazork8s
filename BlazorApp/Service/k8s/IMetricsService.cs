using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Utils;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IMetricsService
{
    Task<bool>                MetricsServerReady();
    Task<IList<NodeMetrics>>  NodeMetrics();
    Task<IList<PodMetrics>>   PodMetrics();
    MetricsQueue<PodMetrics>  PodMetricsQueue(string  podName);
    MetricsQueue<NodeMetrics> NodeMetricsQueue(string nodeName);
}
