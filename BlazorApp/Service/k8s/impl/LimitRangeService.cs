using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class LimitRangeService : CommonAction<V1LimitRange>, ILimitRangeService
{
    private readonly IBaseService                _baseService;

    public LimitRangeService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
