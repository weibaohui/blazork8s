using System.Collections.Generic;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IClusterRoleService : ICommonAction<V1ClusterRole>
{
    IList<V1Subject> ListManagedSubjectByClusterRole(V1ClusterRole role);
    IList<V1Subject> ListManagedSubjectByClusterRoleName(string    name);
}
