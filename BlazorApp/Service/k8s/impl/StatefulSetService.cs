using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class StatefulSetService : CommonAction<V1StatefulSet>, IStatefulSetService
{
    private readonly IBaseService                _baseService;

    public StatefulSetService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
