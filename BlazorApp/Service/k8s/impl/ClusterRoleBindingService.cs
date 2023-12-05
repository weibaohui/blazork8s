using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ClusterRoleBindingService : CommonAction<V1ClusterRoleBinding>, IClusterRoleBindingService
{
    private readonly IBaseService                _baseService;

    public ClusterRoleBindingService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
