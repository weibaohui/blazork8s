using System.Threading.Tasks;
using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s.impl;

public class UdpRouteService(IKubeService kubeService) : CommonAction<V1Alpha2UDPRoute>, IUdpRouteService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return Task.CompletedTask;

        // return await kubeService.Client().DeleteNamespacedUdpRouteAsync(name, ns);
    }
}