using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ResourceQuotaService : CommonAction<V1ResourceQuota>, IResourceQuotaService
{
    private readonly IKubeService                _kubeService;

    public ResourceQuotaService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedResourceQuotaAsync(name, ns);
    }
}
