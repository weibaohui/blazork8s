using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class NetworkPolicyService : CommonAction<V1NetworkPolicy>, INetworkPolicyService
{
    private readonly IKubeService                _baseService;

    public NetworkPolicyService(IKubeService baseService)
    {
        _baseService = baseService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteNamespacedNetworkPolicyAsync(name, ns);
    }
}
