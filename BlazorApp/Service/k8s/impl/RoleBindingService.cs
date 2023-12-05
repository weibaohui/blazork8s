using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class RoleBindingService : CommonAction<V1RoleBinding>, IRoleBindingService
{
    private readonly IBaseService                _baseService;

    public RoleBindingService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
