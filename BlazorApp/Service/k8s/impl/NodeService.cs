using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class NodeService : CommonAction<V1Node>, INodeService
    {
        private readonly IKubeService _kubeService;

        public NodeService(IKubeService kubeService)
        {
            _kubeService = kubeService;
        }

        public new async Task<V1Status> Delete(string ns, string name)
        {
           return await _kubeService.Client().DeleteNodeAsync(name: name);
        }
    }
}
