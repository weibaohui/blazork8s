using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ReplicationControllerService : CommonAction<V1ReplicationController>, IReplicationControllerService
{
    private readonly IKubeService                _baseService;

    public ReplicationControllerService(IKubeService baseService)
    {
        _baseService = baseService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteNamespacedReplicationControllerAsync(name, ns);
    }
}
