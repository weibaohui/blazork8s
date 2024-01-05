using System.Collections.Generic;
using System.Linq;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class RoleService : CommonAction<V1Role>, IRoleService
{
    private readonly IBaseService                _baseService;

    private readonly IRoleBindingService _roleBindingService;

    public RoleService(IBaseService baseService, IRoleBindingService roleBindingService)
    {
        _baseService             = baseService;
        _roleBindingService = roleBindingService;
    }




    public IList<V1Subject> ListManagedSubjectByRole(V1Role role)
    {
        var bindings = _roleBindingService.List()
            .Where(x =>
                x.Namespace()== role.Namespace() &&
                x.RoleRef is not null && x.RoleRef.Name == role.Name()
            )
            .ToList();
        return bindings.Where(x => x.Subjects is { Count: > 0 }).SelectMany(x => x.Subjects).ToList();
    }
}
