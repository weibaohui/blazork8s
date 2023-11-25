using System.Collections.Generic;
using Entity;
using k8s.Models;

namespace BlazorApp.Service.k8s
{
    public interface INodeService : ICommonAction<V1Node>
    {
        V1Node                           FilterByNodeName(string             name);
        (V1Node node, IList<V1Pod> pods) GetNodeWithPodListByNodeName(string nodeName);

        /// <summary>
        /// 将Node以及在Node上运行的Pod组装在一起
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        NodeVO GetNodeVOWithPodListByNodeName(string nodeName);
    }
}