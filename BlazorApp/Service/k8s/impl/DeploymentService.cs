using System.Threading.Tasks;
using k8s;
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

        public new async Task<object> Delete(string ns, string name)
        {
            return await _baseService.Client().AppsV1.DeleteNamespacedDeploymentAsync(name, ns);
        }
    }
}
