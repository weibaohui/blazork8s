using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s;

public interface IHttpRouteService : ICommonAction<V1HTTPRoute>
{
}