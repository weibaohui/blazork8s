using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ConfigMapService(IKubeService kubeService) : CommonAction<V1ConfigMap>, IConfigMapService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedConfigMapAsync(name, ns);
    }
}
