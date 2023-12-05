using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ResourceQuotaService : CommonAction<V1ResourceQuota>, IResourceQuotaService
{
    private readonly IBaseService                _baseService;

    public ResourceQuotaService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
