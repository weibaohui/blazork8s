using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class PersistentVolumeClaimService : CommonAction<V1PersistentVolumeClaim>, IPersistentVolumeClaimService
{
    private readonly IKubeService                _kubeService;

    public PersistentVolumeClaimService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedPersistentVolumeClaimAsync(name, ns);
    }
}
