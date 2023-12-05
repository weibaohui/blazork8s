using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class CronJobService : CommonAction<V1CronJob>, ICronJobService
{
    private readonly IBaseService                _baseService;

    public CronJobService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
