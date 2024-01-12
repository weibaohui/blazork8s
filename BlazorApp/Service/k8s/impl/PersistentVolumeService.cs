using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class PersistentVolumeService : CommonAction<V1PersistentVolume>, IPersistentVolumeService
{
    private readonly IKubeService _kubeService;

    public PersistentVolumeService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }

    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeletePersistentVolumeAsync(name);
    }
}
