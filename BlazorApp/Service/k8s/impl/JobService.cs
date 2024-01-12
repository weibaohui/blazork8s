using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class JobService : CommonAction<V1Job>, IJobService
{
    private readonly IKubeService                _baseService;

    public JobService(IKubeService baseService)
    {
        _baseService = baseService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteNamespacedJobAsync(name, ns);
    }
}
