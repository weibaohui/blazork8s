using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ServiceAccountService : CommonAction<V1ServiceAccount>, IServiceAccountService
{
    private readonly IBaseService                _baseService;

    public ServiceAccountService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
