using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace Blazor.Service
{
    public interface IPodService : INamespaceAction<V1Pod>
    {
        Task<V1PodList>    List();
        Task<V1PodList>    ListByNamespace(string ns);
        Task<int>          NodePodsNum();
        Task               ShowPodDrawer(V1Pod   pod);
        Task<IList<V1Pod>> ListByOwnerUid(string controllerByUid);
        //更新Pod
        void UpdateSharePods(WatchEventType           type, V1Pod item);
    }
}
