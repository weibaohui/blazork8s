using System.Collections.Generic;
using System.Threading.Tasks;
using Extension.k8s;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.impl
{
    public class NodeService : INodeService
    {
        private readonly IBaseService  _baseService;
        private readonly IPodService   _podService;

        public NodeService(IBaseService baseService, IPodService podService)
        {
            _baseService   = baseService;
            _podService    = podService;
        }

        public async Task<(V1Node node, IList<V1Pod> pods)> GetNodeWithPodListByNodeName(string nodeName)
        {
            var nodeList = await List();
            var node     = nodeList.FilterByNodeName(nodeName);
            var podList  = await _podService.List();
            var pods     = podList.FilterByNodeName(nodeName);
            return (node, pods);
        }



        public async Task<V1NodeList> List()
        {
            return await _baseService.Client().ListNodeAsync();
        }

        public async Task<V1Node> FilterByNodeName(string name)
        {
            var list = await List();
            return list.FilterByNodeName(name);
        }
    }
}
