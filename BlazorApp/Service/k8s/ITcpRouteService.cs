using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s;

public interface ITcpRouteService : ICommonAction<V1Alpha2TCPRoute>
{
}