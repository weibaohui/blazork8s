using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s;

public interface ITlsRouteService : ICommonAction<V1Alpha2TLSRoute>
{
}