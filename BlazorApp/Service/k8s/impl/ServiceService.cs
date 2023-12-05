using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ServiceService : CommonAction<V1Service>, IServiceService
{
    private readonly IBaseService                _baseService;

    public ServiceService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
