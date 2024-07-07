using System.Threading.Tasks;
using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s.impl;

public class GrpcRouteService(IKubeService kubeService) : CommonAction<V1GRPCRoute>, IGrpcRouteService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return Task.CompletedTask;

        // return await kubeService.Client().DeleteNamespacedGrpcRouteAsync(name, ns);
    }
}