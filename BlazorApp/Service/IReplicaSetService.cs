using k8s.Models;

namespace BlazorApp.Service
{
    public interface IReplicaSetService : ICommonAction<V1ReplicaSet>
    {
    }
}
