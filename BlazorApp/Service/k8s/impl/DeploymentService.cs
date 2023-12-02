using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class DeploymentService : CommonAction<V1Deployment>, IDeploymentService
    {
        private readonly IBaseService                _baseService;

        public DeploymentService(IBaseService baseService)
        {
            _baseService = baseService;
        }
    }
}
