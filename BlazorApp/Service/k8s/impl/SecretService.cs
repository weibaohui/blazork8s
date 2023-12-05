using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class SecretService : CommonAction<V1Secret>, ISecretService
{
    private readonly IBaseService                _baseService;

    public SecretService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
