using System.Collections.Generic;
using Entity.Crd.Gateway;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IUdpRouteService : ICommonAction<V1Alpha2UDPRoute>
{
    IList<V1Alpha2UDPRoute> ListByServiceList(List<V1Service> services);
    IList<V1Service> GetBackendServices(V1Alpha2UDPRoute udpRoute);
}
