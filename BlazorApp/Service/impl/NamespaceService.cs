using k8s.Models;

namespace BlazorApp.Service.impl
{
    public class NamespaceService : CommonAction<V1Namespace>, INamespaceService
    {
        private readonly IBaseService _baseService;

        public NamespaceService(IBaseService baseService)
        {
            _baseService = baseService;
        }
    }
}