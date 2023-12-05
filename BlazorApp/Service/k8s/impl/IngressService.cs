using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class IngressService : CommonAction<V1Ingress>, IIngressService
{
    private readonly IBaseService                _baseService;

    public IngressService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
