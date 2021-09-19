using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using Blazor.Pages.Node;
using Entity;
using Extension.k8s;
using k8s.Models;

namespace Blazor.Service.impl
{
    public class NodeService : INodeService
    {
        private readonly IBaseService  BaseService;
        private readonly IPodService   PodService;
        private readonly DrawerService DrawerService;

        public NodeService(IBaseService baseService, IPodService podService, DrawerService drawerService)
        {
            BaseService   = baseService;
            PodService    = podService;
            DrawerService = drawerService;
        }

        public async Task<(V1Node node, IList<V1Pod> pods)> DrawerNodeDetail(string nodeName)
        {
            var nodeList = await List();
            var node     = nodeList.FilterByNodeName(nodeName);
            var podList  = await PodService.List();
            var pods     = podList.FilterByNodeName(nodeName);
            return (node, pods);
        }

        public async Task ShowNodeDrawer(V1Node node, IList<V1Pod> pods)
        {
            var options = new DrawerOptions
            {
                Title = "Node:" + node.Name(),
                Width = 800
            };
            await DrawerService.CreateAsync<NodeDetailView, NodeVO, bool>(options,
                new NodeVO { Node = node, Pods = pods });
        }

        public async Task<V1NodeList> List()
        {
            return await BaseService.GetFromJsonAsync<V1NodeList>("/KubeApi/api/v1/nodes/");
        }

        public async Task<V1Node> FilterByNodeName(string name)
        {
            var list = await List();
            return list.FilterByNodeName(name);
        }
    }
}
