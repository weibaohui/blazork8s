using BlazorApp.Utils;
using k8s.Models;

namespace BlazorApp.Service.impl
{
    public class DeploymentService : CommonAction<V1Deployment>, IDeploymentService
    {
        private readonly IBaseService                _baseService;
        private readonly ResourceCache<V1Deployment> _cache = ResourceCacheHelper<V1Deployment>.Instance.Build();

        public DeploymentService(IBaseService baseService)
        {
            _baseService = baseService;
        }
    }
}