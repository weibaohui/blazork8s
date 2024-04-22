using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Utils.Prometheus.Models.Interfaces;
using Entity.Analyze;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface INodeService : ICommonAction<V1Node>
{
    Task Cordon(string nodeName);
    Task UnCordon(string nodeName);
    int GetPodCount(string nodeName);
    string GetPodCapacity(string nodeName);
    string GetMemoryCapacity(string nodeName);
    string GetCpuCapacity(string nodeName);
    Task<List<Result>> Analyze();
    Task<List<IMetric>> GetMetrics(string nodeName);
    Task<List<IMetric>> GetCadvisorMetrics(string nodeName);
    Task<List<IMetric>> GetResourceMetrics(string nodeName);
    Task<List<IMetric>> GetProbesMetrics(string nodeName);
}
