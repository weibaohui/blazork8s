using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Node;
using Entity;
using Extension.k8s;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.impl
{
    public class NodeService : INodeService
    {
        private readonly IBaseService  _baseService;
        private readonly IPodService   _podService;
        private readonly DrawerService _drawerService;

        public NodeService(IBaseService baseService, IPodService podService, DrawerService drawerService)
        {
            _baseService   = baseService;
            _podService    = podService;
            _drawerService = drawerService;
        }

        public async Task<(V1Node node, IList<V1Pod> pods)> DrawerNodeDetail(string nodeName)
        {
            var nodeList = await List();
            var node     = nodeList.FilterByNodeName(nodeName);
            var podList  = await _podService.List();
            var pods     = podList.FilterByNodeName(nodeName);
            return (node, pods);
        }

        public async Task ShowNodeDrawer(string nodeName)
        {
            var nodeDetail = await DrawerNodeDetail(nodeName);
            await ShowNodeDrawer(nodeDetail.node, nodeDetail.pods);
        }

        public async Task ShowNodeDrawer(V1Node node, IList<V1Pod> pods)
        {
            var options = new DrawerOptions
            {
                Title = "Node:" + node.Name(),
                Width = 800
            };
            await _drawerService.CreateAsync<NodeDetailView, NodeVO, bool>(options,
                new NodeVO { Node = node, Pods = pods });
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
