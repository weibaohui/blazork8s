using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class SecretService : CommonAction<V1Secret>, ISecretService
{
    private readonly IKubeService                _kubeService;

    public SecretService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedSecretAsync(name, ns);
    }
}
