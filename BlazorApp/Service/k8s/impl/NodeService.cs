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
    }
}
