using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service;

public interface IWatchService
{
    Task<IList<V1Pod>> ListPods();
    Task               WatchAllPod();

    /// <summary>
    /// Pod列表是否发生变更
    /// </summary>
    /// <returns></returns>
    bool PodListChangedByWatch();

    //更新Pod
    void UpdateSharePods(WatchEventType type, V1Pod item);
}
