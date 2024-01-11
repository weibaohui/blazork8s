using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IStatefulSetService : ICommonAction<V1StatefulSet>
{
    Task UpdateReplicas(V1StatefulSet item, int? replicas);

}
