using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class EndpointSliceService(IKubeService kubeService) : CommonAction<V1EndpointSlice>, IEndpointSliceService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedEndpointSliceAsync(name, ns);
    }
}
