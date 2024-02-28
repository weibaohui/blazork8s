using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Analyze;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IStatefulSetService : ICommonAction<V1StatefulSet>
{
    Task UpdateReplicas(V1StatefulSet item, int? replicas);

    Task Restart(V1StatefulSet item);

    Task<List<Result>> Analyze();

}
