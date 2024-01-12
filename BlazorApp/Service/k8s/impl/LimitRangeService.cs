using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class LimitRangeService : CommonAction<V1LimitRange>, ILimitRangeService
{
    private readonly IKubeService                _kubeService;

    public LimitRangeService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedLimitRangeAsync(name, ns);
    }
}
