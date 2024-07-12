using System.Threading.Tasks;
using Entity.Crd.Gateway;
using k8s;

namespace BlazorApp.Service.k8s.impl;

public class GatewayClassService(IKubeService kubeService) : CommonAction<V1GatewayClass>, IGatewayClassService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().CustomObjects
            .DeleteClusterCustomObjectAsync("gateway.networking.k8s.io", "v1", "gatewayclasses", name);
    }
}
