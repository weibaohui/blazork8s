using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class NamespaceService(IKubeService kubeService) : CommonAction<V1Namespace>, INamespaceService
    {
        public new async Task<object> Delete(string ns, string name)
        {
            return await kubeService.Client().DeleteNamespaceAsync(name);
        }
    }
}
