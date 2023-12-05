using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IPersistentVolumeService : ICommonAction<V1PersistentVolume>
{
}
