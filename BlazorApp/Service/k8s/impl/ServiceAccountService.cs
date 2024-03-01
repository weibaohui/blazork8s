using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ServiceAccountService(
    IKubeService               kubeService,
    IRoleBindingService        roleBindingService,
    IClusterRoleBindingService clusterRoleBindingService)
    : CommonAction<V1ServiceAccount>, IServiceAccountService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedServiceAccountAsync(name, ns);
    }
    public IList<V1RoleRef> ListRoles(string serviceAccountName)
    {
        var bindings = roleBindingService.List()
            .Where(x =>
                x.Subjects is { Count: > 0 }
                && x.Subjects.Any(y => y.Kind == "ServiceAccount" && y.Name == serviceAccountName)
            )
            .ToList();
        return bindings.Select(x => x.RoleRef).ToList();
    }

    public IList<V1RoleRef> ListClusterRoles(string serviceAccountName)
    {
        var bindings = clusterRoleBindingService.List()
            .Where(x =>
                x.Subjects is { Count: > 0 }
                && x.Subjects.Any(y => y.Kind == "ServiceAccount" && y.Name == serviceAccountName)
            )
            .ToList();
        return bindings.Select(x => x.RoleRef).ToList();
    }
}
