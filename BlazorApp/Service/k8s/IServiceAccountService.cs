using System.Collections.Generic;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IServiceAccountService : ICommonAction<V1ServiceAccount>
{
    IList<V1RoleRef> ListRoles(string        serviceAccountName);
    IList<V1RoleRef> ListClusterRoles(string serviceAccountName);
}
