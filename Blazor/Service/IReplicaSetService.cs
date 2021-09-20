using System.Threading.Tasks;
using k8s.Models;

namespace Blazor.Service
{
    public interface IReplicaSetService : INamespaceAction<V1ReplicaSet>
    {
        Task<V1ReplicaSetList> List();
        Task<V1ReplicaSetList> ListByNamespace(string ns);
        Task            ShowPodDrawer(V1ReplicaSet rs);
    }
}
