using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class SecretService : CommonAction<V1Secret>, ISecretService
{
    private readonly IKubeService                _baseService;

    public SecretService(IKubeService baseService)
    {
        _baseService = baseService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteNamespacedSecretAsync(name, ns);
    }
}
