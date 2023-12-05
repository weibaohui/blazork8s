using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class EndpointsService : CommonAction<V1Endpoints>, IEndpointsService
{
    private readonly IBaseService                _baseService;

    public EndpointsService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
