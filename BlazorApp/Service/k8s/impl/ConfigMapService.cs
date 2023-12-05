using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ConfigMapService : CommonAction<V1ConfigMap>, IConfigMapService
{
    private readonly IBaseService                _baseService;

    public ConfigMapService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
