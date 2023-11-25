using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service.k8s
{
    public interface IPodService : ICommonAction<V1Pod>
    {
        IEnumerable<Tuple<string, int>> NodePodsNum();

        IList<V1Pod> ListByNodeName(string nodeName);


        //删除POD
        Task<bool> DeletePod(string ns, string name);

        Task<Stream>    Logs(V1Pod      pod, bool   follow = false, bool previous = false);
        Task<WebSocket> ExecInPod(V1Pod pod, string command);
    }
}