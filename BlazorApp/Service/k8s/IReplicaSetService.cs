using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IReplicaSetService : ICommonAction<V1ReplicaSet>
{
    Task UpdateReplicas(V1ReplicaSet item, int? replicas);

}
