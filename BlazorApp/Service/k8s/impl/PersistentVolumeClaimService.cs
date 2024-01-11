using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class PersistentVolumeClaimService : CommonAction<V1PersistentVolumeClaim>, IPersistentVolumeClaimService
{
    private readonly IBaseService                _baseService;

    public PersistentVolumeClaimService(IBaseService baseService)
    {
        _baseService = baseService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteNamespacedPersistentVolumeClaimAsync(name, ns);
    }
}
