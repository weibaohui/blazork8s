using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ReplicationControllerService : CommonAction<V1ReplicationController>, IReplicationControllerService
{
    private readonly IKubeService                _kubeService;

    public ReplicationControllerService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedReplicationControllerAsync(name, ns);
    }
}
