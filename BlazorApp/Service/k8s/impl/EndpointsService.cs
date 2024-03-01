using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class EndpointsService(IKubeService kubeService) : CommonAction<V1Endpoints>, IEndpointsService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedEndpointsAsync(name, ns);
    }
}
