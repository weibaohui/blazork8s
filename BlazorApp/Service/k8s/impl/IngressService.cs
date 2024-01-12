using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class IngressService : CommonAction<V1Ingress>, IIngressService
{
    private readonly IKubeService                _kubeService;

    public IngressService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedIngressAsync(name, ns);
    }
}
