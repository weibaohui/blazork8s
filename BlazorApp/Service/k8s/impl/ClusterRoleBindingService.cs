using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ClusterRoleBindingService : CommonAction<V1ClusterRoleBinding>, IClusterRoleBindingService
{
    private readonly IKubeService                _baseService;

    public ClusterRoleBindingService(IKubeService baseService)
    {
        _baseService = baseService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteClusterRoleBindingAsync(name);
    }
}
