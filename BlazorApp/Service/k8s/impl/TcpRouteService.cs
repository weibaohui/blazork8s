using System.Threading.Tasks;
using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s.impl;

public class TcpRouteService(IKubeService kubeService) : CommonAction<V1Alpha2TCPRoute>, ITcpRouteService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return Task.CompletedTask;

        // return await kubeService.Client().DeleteNamespacedTcpRouteAsync(name, ns);
    }
}