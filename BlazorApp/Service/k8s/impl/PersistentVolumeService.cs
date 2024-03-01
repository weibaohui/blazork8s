using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class PersistentVolumeService(IKubeService kubeService)
    : CommonAction<V1PersistentVolume>, IPersistentVolumeService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeletePersistentVolumeAsync(name);
    }
}
