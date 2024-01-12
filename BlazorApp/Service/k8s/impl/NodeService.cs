using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class NodeService : CommonAction<V1Node>, INodeService
    {
        private readonly IKubeService _baseService;

        public NodeService(IKubeService baseService)
        {
            _baseService = baseService;
        }

        public new async Task<V1Status> Delete(string ns, string name)
        {
           return await _baseService.Client().DeleteNodeAsync(name: name);
        }
    }
}
