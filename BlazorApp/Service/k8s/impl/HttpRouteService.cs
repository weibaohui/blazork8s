using System.Threading.Tasks;
using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s.impl;

public class HttpRouteService(IKubeService kubeService) : CommonAction<V1HTTPRoute>, IHttpRouteService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return Task.CompletedTask;

        // return await kubeService.Client().DeleteNamespacedHttpRouteAsync(name, ns);
    }
}