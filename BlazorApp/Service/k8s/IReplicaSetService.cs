using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Analyze;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IReplicaSetService : ICommonAction<V1ReplicaSet>
{
    Task UpdateReplicas(V1ReplicaSet item, int? replicas);
    Task<List<Result>> Analyze();
}
