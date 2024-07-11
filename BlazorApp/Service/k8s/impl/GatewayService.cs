using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Crd.Gateway;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class GatewayService(IKubeService kubeService) : CommonAction<V1Gateway>, IGatewayService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return Task.CompletedTask;

        // return await kubeService.Client().DeleteNamespacedGatewayAsync(name, ns);
    }

    public IList<V1Gateway> ListByParentRefs(IList<ParentReference> parentRefs)
    {
        var list = new List<V1Gateway>();
        foreach (var svc in parentRefs)
        {
            var ns = svc.Namespace;
            var name = svc.Name;
            var result = List().Where(x => x.Name() == name && svc.Kind == x.Kind)
                .ToList();
            list.AddRange(result);
        }

        return list;
    }
}
