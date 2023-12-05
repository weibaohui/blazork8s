using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class RoleService : CommonAction<V1Role>, IRoleService
{
    private readonly IBaseService                _baseService;

    public RoleService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
