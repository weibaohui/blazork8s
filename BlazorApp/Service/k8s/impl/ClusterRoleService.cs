using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ClusterRoleService : CommonAction<V1ClusterRole>, IClusterRoleService
{
    private readonly IBaseService               _baseService;
    private readonly IClusterRoleBindingService _clusterRoleBindingService;

    public ClusterRoleService(IBaseService baseService, IClusterRoleBindingService clusterRoleBindingService)
    {
        _baseService               = baseService;
        _clusterRoleBindingService = clusterRoleBindingService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteClusterRoleAsync(name);
    }

    public IList<V1Subject> ListManagedSubjectByClusterRole(V1ClusterRole role)
    {
        return ListManagedSubjectByClusterRoleName(role.Name());
    }

    public IList<V1Subject> ListManagedSubjectByClusterRoleName(string name)
    {

        var bindings = _clusterRoleBindingService.List()
            .Where(x =>
                x.RoleRef is not null && x.RoleRef.Name == name
            )
            .ToList();
        return bindings.Where(x => x.Subjects is { Count: > 0 }).SelectMany(x => x.Subjects).ToList();
    }
}
