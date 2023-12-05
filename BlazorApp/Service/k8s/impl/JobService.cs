using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class JobService : CommonAction<V1Job>, IJobService
{
    private readonly IBaseService                _baseService;

    public JobService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
