using System.Threading.Tasks;
using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s.impl;

public class GatewayService(IKubeService kubeService) : CommonAction<V1Gateway>, IGatewayService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return Task.CompletedTask;

        // return await kubeService.Client().DeleteNamespacedGatewayAsync(name, ns);
    }
}