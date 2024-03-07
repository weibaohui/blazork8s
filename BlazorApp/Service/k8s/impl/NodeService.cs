using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Utils;
using BlazorApp.Utils.Prometheus.Models.Interfaces;
using Entity.Analyze;
using Extension;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class NodeService(IKubeService kubeService, IPodService podService, IMetricsService metricsService)
        : CommonAction<V1Node>, INodeService
    {
        public new async Task<V1Status> Delete(string ns, string name)
        {
            return await kubeService.Client().DeleteNodeAsync(name: name);
        }

        public async Task Cordon(string nodeName)
        {
            await SetSchedulable(nodeName, false); //cordon()
        }

        public async Task UnCordon(string nodeName)
        {
            await SetSchedulable(nodeName, true); //uncordon()
        }

        private async Task SetSchedulable(string nodeName, bool schedulable)
        {
            var node = GetByName(nodeName);
            var unScheduleStr = """
                                {
                                    "spec": {
                                        "unschedulable":  true
                                    }
                                }
                                """;
            var scheduleStr = """
                              {
                                  "spec": {
                                      "unschedulable":  false
                                  }
                              }
                              """;
            await kubeService.Client()
                .PatchNodeAsync(new V1Patch(schedulable ? scheduleStr : unScheduleStr, V1Patch.PatchType.MergePatch),
                    node.Name());
        }

        public long GetPodCount(string nodeName)
        {
            return podService.ListByNodeName(nodeName).ToList().Count;
        }

        public string GetPodCapacity(string nodeName)
        {
            var capacity = GetNodeCapacity(nodeName);
            var pods     = capacity?.FirstOrDefault(x => x.Key == "pods").Value.CanonicalizeString();
            var current  = GetPodCount(nodeName);
            return $"{current}/{pods}";
        }

        private IDictionary<string, ResourceQuantity> GetNodeCapacity(string nodeName)
        {
            var capacity = this.List().FirstOrDefault(x => x.Name() == nodeName)?.Status.Capacity;
            return capacity;
        }

        public string GetCpuCapacity(string nodeName)
        {
            var capacity = GetNodeCapacity(nodeName);
            return ResourceCurrentAndCapacity(nodeName, "cpu");
        }

        public string GetMemoryCapacity(string nodeName)
        {
            var capacity = GetNodeCapacity(nodeName);
            return ResourceCurrentAndCapacity(nodeName, "memory");
        }

        private string ResourceCurrentAndCapacity(string nodeName, string type)
        {
            var capacity = GetNodeCapacity(nodeName);
            var all      = capacity?.FirstOrDefault(x => x.Key == type).Value.HumanizeValue();

            var current = metricsService.NodeMetricsQueue(nodeName).GetList()
                .FirstOrDefault()?.Usage
                .FirstOrDefault(x => x.Key == type).Value.HumanizeValue();

            return $"{current}/{all}";
        }

        public async Task<List<IMetric>> GetMetrics(string nodeName)
        {
            var             metricString = await kubeService.GetStringAsync($"/api/v1/nodes/{nodeName}/proxy/metrics");
            return await kubeService.ConvertStringToMetrics(metricString);
        }

        public async Task<List<IMetric>> GetCadvisorMetrics(string nodeName)
        {
            var metricString = await kubeService.GetStringAsync($"/api/v1/nodes/{nodeName}/proxy/metrics/cadvisor");
            return await kubeService.ConvertStringToMetrics(metricString);
        }

        public async Task<List<IMetric>> GetResourceMetrics(string nodeName)
        {
            var metricString = await kubeService.GetStringAsync($"/api/v1/nodes/{nodeName}/proxy/metrics/resource");
            return await kubeService.ConvertStringToMetrics(metricString);
        }

        public async Task<List<IMetric>> GetProbesMetrics(string nodeName)
        {
            var metricString = await kubeService.GetStringAsync($"/api/v1/nodes/{nodeName}/proxy/metrics/probes");
            return await kubeService.ConvertStringToMetrics(metricString);
        }

        public async Task<List<Result>> Analyze()
        {
            var items   = List();
            var results = new List<Result>();
            foreach (var item in items.ToList())
            {
                var failures = new List<Failure>();

                if (item.Status.Conditions is { Count: > 0 })
                {
                    foreach (var status in item.Status.Conditions)
                    {
                        if (status.Type == "Ready" && status.Status == "False")
                        {
                            //节点Ready类型为False
                            failures.Add(new Failure
                            {
                                Text =
                                    $"{item.Name()} has condition of type {status.Type}, reason {status.Reason} {status.Message}"
                            });
                        }

                        if (status.Type.Contains("Pressure") && status.Status == "True")
                        {
                            //压力类型，正常应该为False，无压力为正常
                            failures.Add(new Failure
                            {
                                Text =
                                    $"{item.Name()} has condition of PressureType {status.Type}, reason {status.Reason} {status.Message}"
                            });
                        }

                        if (status.Type.Contains("NetworkUnavailable") && status.Status != "False")
                        {
                            //网络不可达状态，正常应该为False
                            failures.Add(new Failure
                            {
                                Text =
                                    $"{item.Name()} has condition of UnavailableType {status.Type}, reason {status.Reason} {status.Message}"
                            });
                        }
                    }
                }

                if (failures.Count > 0)
                {
                    results.Add(Result.NewResult(item, failures));
                }
            }

            if (results.Count == 0)
            {
                ClusterInspectionResultContainer.Instance.GetPassResources().Add("Node");
            }

            ClusterInspectionResultContainer.Instance.AddResourcesCount("Node", items.ToList().Count);
            return results;
        }
    }
}
