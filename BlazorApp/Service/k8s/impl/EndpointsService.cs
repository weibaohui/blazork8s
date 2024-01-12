using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class EndpointsService : CommonAction<V1Endpoints>, IEndpointsService
{
    private readonly IKubeService                _kubeService;

    public EndpointsService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedEndpointsAsync(name, ns);
    }
}
