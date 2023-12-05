using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class StorageClassService : CommonAction<V1StorageClass>, IStorageClassService
{
    private readonly IBaseService                _baseService;

    public StorageClassService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
