using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class SecretService(IKubeService kubeService) : CommonAction<V1Secret>, ISecretService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedSecretAsync(name, ns);
    }
}
