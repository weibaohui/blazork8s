using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Crd.Gateway;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class HttpRouteService(IKubeService kubeService) : CommonAction<V1HTTPRoute>, IHttpRouteService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().CustomObjects
            .DeleteNamespacedCustomObjectAsync("gateway.networking.k8s.io", "v1", ns, "httproutes", name);
    }

    public IList<V1HTTPRoute> ListByServiceList(List<V1Service> services)
    {
        var list = new List<V1HTTPRoute>();
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
}
