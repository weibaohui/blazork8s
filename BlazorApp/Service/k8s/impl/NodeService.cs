using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class NodeService : CommonAction<V1Node>, INodeService
    {
        private readonly IBaseService _baseService;

        public NodeService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public new async Task<V1Status> Delete(string ns, string name)
        {
           return await _baseService.Client().CoreV1.DeleteNodeAsync(name: name);
        }
    }
}
