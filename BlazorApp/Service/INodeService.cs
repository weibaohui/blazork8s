using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using k8s.Models;

namespace BlazorApp.Service
{
    public interface INodeService
    {
        Task<V1NodeList>                       List();
        Task<V1Node>                           FilterByNodeName(string             name);
        Task<(V1Node node, IList<V1Pod> pods)> GetNodeWithPodListByNodeName(string nodeName);

        /// <summary>
        /// 将Node以及在Node上运行的Pod组装在一起
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        Task<NodeVO> GetNodeVOWithPodListByNodeName(string nodeName);
    }
}
