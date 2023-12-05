using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class EndpointSliceService : CommonAction<V1EndpointSlice>, IEndpointSliceService
{
    private readonly IBaseService                _baseService;

    public EndpointSliceService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
