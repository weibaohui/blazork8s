using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class EndpointSliceService : CommonAction<V1EndpointSlice>, IEndpointSliceService
{
    private readonly IKubeService                _kubeService;

    public EndpointSliceService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedEndpointSliceAsync(name, ns);
    }
}
