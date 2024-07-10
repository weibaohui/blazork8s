using System.Collections.Generic;
using Entity.Crd.Gateway;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface ITcpRouteService : ICommonAction<V1Alpha2TCPRoute>
{
    IList<V1Alpha2TCPRoute> ListByServiceList(List<V1Service> services);
}
