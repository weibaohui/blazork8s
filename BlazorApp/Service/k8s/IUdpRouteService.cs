using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s;

public interface IUdpRouteService : ICommonAction<V1Alpha2UDPRoute>
{
}