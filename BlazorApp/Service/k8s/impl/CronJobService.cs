using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class CronJobService : CommonAction<V1CronJob>, ICronJobService
{
    private readonly IKubeService                _baseService;

    public CronJobService(IKubeService baseService)
    {
        _baseService = baseService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteNamespacedCronJobAsync(name, ns);
    }
}
