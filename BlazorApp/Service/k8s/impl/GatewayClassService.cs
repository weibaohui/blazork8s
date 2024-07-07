using System.Threading.Tasks;
using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s.impl;

public class GatewayClassService(IKubeService kubeService) : CommonAction<V1GatewayClass>, IGatewayClassService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return Task.CompletedTask;

        // return await kubeService.Client().DeleteNamespacedGatewayClassAsync(name, ns);
    }
}