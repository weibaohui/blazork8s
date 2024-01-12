using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class JobService : CommonAction<V1Job>, IJobService
{
    private readonly IKubeService                _kubeService;

    public JobService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedJobAsync(name, ns);
    }
}
