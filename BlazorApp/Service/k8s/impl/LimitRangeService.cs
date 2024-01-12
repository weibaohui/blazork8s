using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class LimitRangeService : CommonAction<V1LimitRange>, ILimitRangeService
{
    private readonly IKubeService                _baseService;

    public LimitRangeService(IKubeService baseService)
    {
        _baseService = baseService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteNamespacedLimitRangeAsync(name, ns);
    }
}
