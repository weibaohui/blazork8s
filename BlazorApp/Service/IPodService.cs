using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service
{
    public interface IPodService : INamespaceAction<V1Pod>
    {
        Task<IList<V1Pod>> ListPods();
        Task<int>          NodePodsNum();

        Task<IList<V1Pod>> ListByOwnerUid(string controllerByUid);


        //删除POD
        Task<bool> DeletePod(string ns, string name);

        Task<Stream>    Logs(V1Pod      pod, bool   follow = false, bool previous = false);
        Task<WebSocket> ExecInPod(V1Pod pod, string command);

        /// <summary>
        /// Pod列表是否发生变更
        /// </summary>
        /// <returns></returns>
        bool Changed();
    }
}
