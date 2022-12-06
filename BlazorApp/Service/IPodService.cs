using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service
{
    public interface IPodService : INamespaceAction<V1Pod>
    {
        Task<List<V1Pod>> List();
        Task<int>         NodePodsNum();

        Task<IList<V1Pod>> ListByOwnerUid(string controllerByUid);

        //更新Pod
        void UpdateSharePods(WatchEventType type, V1Pod item);

        //删除POD
        Task<bool> DeletePod(string ns, string name);

        Task Logs(V1Pod pod, bool follow = false, bool previous = false);

        Task WatchAllPod();

        /// <summary>
        /// Pod列表是否发生变更
        /// </summary>
        /// <returns></returns>
        Boolean PodListChangedByWatch();
    }
}
