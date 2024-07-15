using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Crd.Gateway;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class TcpRouteService(IKubeService kubeService, IServiceService serviceService)
    : CommonAction<V1Alpha2TCPRoute>, ITcpRouteService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().CustomObjects
            .DeleteNamespacedCustomObjectAsync("gateway.networking.k8s.io", "v1alpha2", ns, "tcproutes", name);
    }

    public IList<V1Alpha2TCPRoute> ListByServiceList(List<V1Service> services)
    {
        var list = new List<V1Alpha2TCPRoute>();
        foreach (var svc in services)
        {
            var ns = svc.Namespace();
            var name = svc.Name();
            var result = List().Where(x => x.Namespace() == ns)
                .Where(x => x.Spec.Rules is { Count: > 0 } && x.Spec.Rules.Any(
                    y => y.BackendRefs is { Count: > 0 } && y.BackendRefs.Any(
                        z => z.Name == name && z.Kind == "Service"
                    ))).ToList();
            list.AddRange(result);
        }

        return list;
    }

    public IList<V1Service> GetBackendServices(V1Alpha2TCPRoute tcpRoute)
    {
        IList<V1Service> svcList = new List<V1Service>();

        var backendRefs = tcpRoute?.Spec?.Rules?
            .SelectMany(x => x.BackendRefs)?
            .Where(x => x.Kind == "Service")?
            .ToList();
        if (backendRefs is { Count: > 0 })
        {
            foreach (var backendRef in backendRefs)
            {
                if (string.IsNullOrWhiteSpace(backendRef.Namespace))
                {
                    backendRef.Namespace = tcpRoute.Namespace();
                }

                var service = serviceService.GetByName(backendRef.Namespace, backendRef.Name);
                if (service is not null)
                {
                    svcList.Add(service);
                }
            }
        }

        return svcList;
    }
}
