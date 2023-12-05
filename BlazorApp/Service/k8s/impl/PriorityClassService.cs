using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class PriorityClassService : CommonAction<V1PriorityClass>, IPriorityClassService
{
    private readonly IBaseService                _baseService;

    public PriorityClassService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
