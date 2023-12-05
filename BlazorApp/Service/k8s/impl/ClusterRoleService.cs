using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ClusterRoleService : CommonAction<V1ClusterRole>, IClusterRoleService
{
    private readonly IBaseService                _baseService;

    public ClusterRoleService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
