using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class RoleBindingService : CommonAction<V1RoleBinding>, IRoleBindingService
{
    private readonly IKubeService                _kubeService;

    public RoleBindingService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedRoleBindingAsync(name, ns);
    }
}
