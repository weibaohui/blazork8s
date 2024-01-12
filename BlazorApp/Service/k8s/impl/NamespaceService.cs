using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class NamespaceService : CommonAction<V1Namespace>, INamespaceService
    {
        private readonly IKubeService _baseService;

        public NamespaceService(IKubeService baseService)
        {
            _baseService = baseService;
        }
        public new async Task<object> Delete(string ns, string name)
        {
            return await _baseService.Client().DeleteNamespaceAsync(name);
        }
    }
}
