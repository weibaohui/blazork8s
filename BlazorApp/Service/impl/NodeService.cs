using System.Collections.Generic;
using Entity;
using Extension.k8s;
using k8s.Models;

namespace BlazorApp.Service.impl
{
    public class NodeService : CommonAction<V1Node>, INodeService
    {
        private readonly IBaseService _baseService;
        private readonly IPodService  _podService;

        public NodeService(IBaseService baseService, IPodService podService)
        {
            _baseService = baseService;
            _podService  = podService;
        }

        public (V1Node node, IList<V1Pod> pods) GetNodeWithPodListByNodeName(string nodeName)
        {
            var nodeList = List();
            var node     = nodeList.FilterByNodeName(nodeName);
            var podList  = _podService.List();
            var pods     = podList.FilterByNodeName(nodeName);
            return (node, pods);
        }


        public NodeVO GetNodeVOWithPodListByNodeName(string nodeName)
        {
            var (node, pods) = GetNodeWithPodListByNodeName(nodeName);
            return new NodeVO { Node = node, Pods = pods };
        }


        public V1Node FilterByNodeName(string name)
        {
            return List().FilterByNodeName(name);
        }
    }
}