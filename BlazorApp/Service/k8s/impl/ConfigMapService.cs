using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ConfigMapService : CommonAction<V1ConfigMap>, IConfigMapService
{
    private readonly IKubeService                _kubeService;

    public ConfigMapService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedConfigMapAsync(name, ns);
    }
}
