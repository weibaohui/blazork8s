using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class RoleService(IKubeService kubeService, IRoleBindingService roleBindingService)
    : CommonAction<V1Role>, IRoleService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedRoleAsync(name, ns);
    }

    public IList<Rbacv1Subject> ListManagedSubjectByRole(V1Role role)
    {
        return this.ListManagedSubjectByRoleName(role.Namespace(), role.Name());
    }

    public IList<Rbacv1Subject> ListManagedSubjectByRoleName(string ns, string name)
    {
        var bindings = roleBindingService.List()
            .Where(x =>
                x.Namespace() == ns &&
                x.RoleRef is not null && x.RoleRef.Name == name
            )
            .ToList();
        return bindings.Where(x => x.Subjects is { Count: > 0 }).SelectMany(x => x.Subjects).ToList();
    }
}
