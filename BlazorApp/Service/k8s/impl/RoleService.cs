using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class RoleService : CommonAction<V1Role>, IRoleService
{
    private readonly IKubeService _kubeService;

    private readonly IRoleBindingService _roleBindingService;

    public RoleService(IKubeService kubeService, IRoleBindingService roleBindingService)
    {
        _kubeService        = kubeService;
        _roleBindingService = roleBindingService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedRoleAsync(name, ns);
    }

    public IList<V1Subject> ListManagedSubjectByRole(V1Role role)
    {
        return this.ListManagedSubjectByRoleName(role.Namespace(), role.Name());
    }

    public IList<V1Subject> ListManagedSubjectByRoleName(string ns, string name)
    {
        var bindings = _roleBindingService.List()
            .Where(x =>
                x.Namespace() == ns &&
                x.RoleRef is not null && x.RoleRef.Name == name
            )
            .ToList();
        return bindings.Where(x => x.Subjects is { Count: > 0 }).SelectMany(x => x.Subjects).ToList();
    }
}
