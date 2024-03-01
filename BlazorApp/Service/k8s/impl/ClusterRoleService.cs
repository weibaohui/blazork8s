using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ClusterRoleService(IKubeService kubeService, IClusterRoleBindingService clusterRoleBindingService)
    : CommonAction<V1ClusterRole>, IClusterRoleService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteClusterRoleAsync(name);
    }

    public IList<Rbacv1Subject> ListManagedSubjectByClusterRole(V1ClusterRole role)
    {
        return ListManagedSubjectByClusterRoleName(role.Name());
    }

    public IList<Rbacv1Subject> ListManagedSubjectByClusterRoleName(string name)
    {

        var bindings = clusterRoleBindingService.List()
            .Where(x =>
                x.RoleRef is not null && x.RoleRef.Name == name
            )
            .ToList();
        return bindings.Where(x => x.Subjects is { Count: > 0 }).SelectMany(x => x.Subjects).ToList();
    }
}
