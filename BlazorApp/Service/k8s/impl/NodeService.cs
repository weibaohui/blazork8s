using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Extension;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class NodeService(IKubeService kubeService, IPodService podService, IMetricsService MetricsService)
        : CommonAction<V1Node>, INodeService
    {
        public new async Task<V1Status> Delete(string ns, string name)
        {
            return await kubeService.Client().DeleteNodeAsync(name: name);
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
            return ResourceCurrentAndCapacity(nodeName,"cpu");
        }
        public string GetMemoryCapacity(string nodeName)
        {
            var capacity = GetNodeCapacity(nodeName);
            return ResourceCurrentAndCapacity(nodeName,"memory");
        }

        private string ResourceCurrentAndCapacity(string nodeName,string type)
        {
            var capacity = GetNodeCapacity(nodeName);
            var all   = capacity?.FirstOrDefault(x => x.Key == type).Value.HumanizeValue();

            var current = MetricsService.NodeMetricsQueue(nodeName).GetList()
                .FirstOrDefault()?.Usage
                .FirstOrDefault(x => x.Key == type).Value.HumanizeValue();

            return $"{current}/{all}";
        }

    }
}
