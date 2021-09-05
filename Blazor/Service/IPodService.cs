using System.Collections.Generic;
using System.Threading.Tasks;
using k8s.Models;

namespace Blazor.Service
{
    public interface IPodService:INamespaceAction<V1Pod>
    {
        Task<V1PodList> List();
        Task<V1PodList> ListByNamespace(string ns);
        Task<int>       NodePodsNum();
        Task<IList<V1Pod>> ListItemsByNamespaceAsync(string ns);

    }
}