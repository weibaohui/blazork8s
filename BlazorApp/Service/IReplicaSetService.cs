using System.Collections.Generic;
using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service
{
    public interface IReplicaSetService : INamespaceAction<V1ReplicaSet>
    {
        Task<V1ReplicaSetList> ListAllReplicaSet();
        Task<V1ReplicaSetList> ListByNamespace(string            ns);
        Task<V1ReplicaSet>     FindByName(string               name);
        Task<IList<V1ReplicaSet>> ListByOwnerUid(string controllerByUid);
    }
}
