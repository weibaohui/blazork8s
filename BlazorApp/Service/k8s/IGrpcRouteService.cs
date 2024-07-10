using System.Collections.Generic;
using Entity.Crd.Gateway;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IGrpcRouteService : ICommonAction<V1GRPCRoute>
{
    IList<V1GRPCRoute> ListByServiceList(List<V1Service> services);
}
