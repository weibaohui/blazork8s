using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IPersistentVolumeClaimService : ICommonAction<V1PersistentVolumeClaim>
{
}
