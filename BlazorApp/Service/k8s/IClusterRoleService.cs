using System.Collections.Generic;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IClusterRoleService : ICommonAction<V1ClusterRole>
{
    IList<Rbacv1Subject> ListManagedSubjectByClusterRole(V1ClusterRole role);
    IList<Rbacv1Subject> ListManagedSubjectByClusterRoleName(string    name);
}
