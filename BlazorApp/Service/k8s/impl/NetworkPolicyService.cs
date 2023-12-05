using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class NetworkPolicyService : CommonAction<V1NetworkPolicy>, INetworkPolicyService
{
    private readonly IBaseService                _baseService;

    public NetworkPolicyService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
