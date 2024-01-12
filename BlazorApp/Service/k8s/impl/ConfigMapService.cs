using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ConfigMapService : CommonAction<V1ConfigMap>, IConfigMapService
{
    private readonly IKubeService                _baseService;

    public ConfigMapService(IKubeService baseService)
    {
        _baseService = baseService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteNamespacedConfigMapAsync(name, ns);
    }
}
