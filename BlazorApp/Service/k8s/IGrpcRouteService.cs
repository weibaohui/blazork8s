using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s;

public interface IGrpcRouteService : ICommonAction<V1GRPCRoute>
{
}