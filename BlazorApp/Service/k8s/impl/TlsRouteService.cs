using System.Threading.Tasks;
using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s.impl;

public class TlsRouteService(IKubeService kubeService) : CommonAction<V1Alpha2TLSRoute>, ITlsRouteService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return Task.CompletedTask;

        // return await kubeService.Client().DeleteNamespacedTlsRouteAsync(name, ns);
    }
}