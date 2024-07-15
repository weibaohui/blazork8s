using System.Collections.Generic;
using Entity.Crd.Gateway;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IHttpRouteService : ICommonAction<V1HTTPRoute>
{
    IList<V1HTTPRoute> ListByServiceList(List<V1Service> v1Services);
    IList<V1Service> GetBackendServices(V1HTTPRoute httpRoute);
}
