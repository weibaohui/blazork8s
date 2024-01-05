using System.Collections.Generic;
using System.Linq;
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


    public IList<V1Subject> ListManagedSubjectByClusterRole(V1ClusterRole role)
    {
        var name = role.Name();
        var bindings = _clusterRoleBindingService.List()
            .Where(x =>
                x.RoleRef is not null && x.RoleRef.Name == name
            )
            .ToList();


        return bindings.Where(x => x.Subjects is { Count: > 0 }).SelectMany(x => x.Subjects).ToList();
    }
}
