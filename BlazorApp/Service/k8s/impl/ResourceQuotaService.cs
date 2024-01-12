using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ResourceQuotaService : CommonAction<V1ResourceQuota>, IResourceQuotaService
{
    private readonly IKubeService                _baseService;

    public ResourceQuotaService(IKubeService baseService)
    {
        _baseService = baseService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteNamespacedResourceQuotaAsync(name, ns);
    }
}
