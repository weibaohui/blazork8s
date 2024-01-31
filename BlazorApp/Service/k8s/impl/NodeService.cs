using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class NodeService(IKubeService kubeService, IPodService podService) : CommonAction<V1Node>, INodeService
    {
        public new async Task<V1Status> Delete(string ns, string name)
        {
            return await kubeService.Client().DeleteNodeAsync(name: name);
        }

        public long GetPodCount(string nodeName)
        {
            return podService.ListByNodeName(nodeName).ToList().Count;
        }

        public string GetPodCapacityCount(string nodeName)
        {
            var capacity = this.List().FirstOrDefault(x => x.Name() == nodeName)?.Status.Capacity;
            var pods     = capacity?.FirstOrDefault(x => x.Key == "pods").Value.CanonicalizeString();
            var current  = GetPodCount(nodeName);
            return $"{current}/{pods}";
        }
    }
}
