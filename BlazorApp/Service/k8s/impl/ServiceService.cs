using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ServiceService : CommonAction<V1Service>, IServiceService
{
    private readonly IKubeService                _kubeService;

    public ServiceService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedServiceAsync(name, ns);
    }
}
