using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ServiceAccountService : CommonAction<V1ServiceAccount>, IServiceAccountService
{
    private readonly IKubeService               _baseService;
    private readonly IClusterRoleBindingService _clusterRoleBindingService;
    private readonly IRoleBindingService        _roleBindingService;

    public ServiceAccountService(IKubeService baseService, IRoleBindingService roleBindingService,
        IClusterRoleBindingService            clusterRoleBindingService)
    {
        _baseService               = baseService;
        _roleBindingService        = roleBindingService;
        _clusterRoleBindingService = clusterRoleBindingService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteNamespacedServiceAccountAsync(name, ns);
    }
    public IList<V1RoleRef> ListRoles(string serviceAccountName)
    {
        var bindings = _roleBindingService.List()
            .Where(x =>
                x.Subjects is { Count: > 0 }
                && x.Subjects.Any(y => y.Kind == "ServiceAccount" && y.Name == serviceAccountName)
            )
            .ToList();
        return bindings.Select(x => x.RoleRef).ToList();
    }

    public IList<V1RoleRef> ListClusterRoles(string serviceAccountName)
    {
        var bindings = _clusterRoleBindingService.List()
            .Where(x =>
                x.Subjects is { Count: > 0 }
                && x.Subjects.Any(y => y.Kind == "ServiceAccount" && y.Name == serviceAccountName)
            )
            .ToList();
        return bindings.Select(x => x.RoleRef).ToList();
    }
}
