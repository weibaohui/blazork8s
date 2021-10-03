using System.Collections.Generic;
using System.Threading.Tasks;
using k8s.Models;

namespace Blazor.Service
{
    public interface IReplicaSetService : INamespaceAction<V1ReplicaSet>
    {
        Task<V1ReplicaSetList> List();
        Task<V1ReplicaSetList> ListByNamespace(string            ns);
        Task                   ShowReplicaSetDrawer(V1ReplicaSet rs);
        Task                   ShowReplicaSetDrawer(string  rsName);
        Task<V1ReplicaSet>     FilterByName(string               name);


        Task<IList<V1ReplicaSet>> ListByOwnerUid(string controllerByUid);
    }
}
