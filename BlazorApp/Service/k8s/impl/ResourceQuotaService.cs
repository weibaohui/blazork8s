using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ResourceQuotaService(IKubeService kubeService) : CommonAction<V1ResourceQuota>, IResourceQuotaService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedResourceQuotaAsync(name, ns);
    }
}
