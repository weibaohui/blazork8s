using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Node
{
    public partial class NodeList : ComponentBase
    {
        [Inject]
        private INodeService NodeService { get; set; }

        [Inject]
        private IPodService PodService { get; set; }

        [Inject]
        private IKubeService KubeService { get; set; }

        [Inject]
        private DrawerService DrawerService { get; set; }

        private IList<V1Node> _nodes;
        private IList<V1Pod>  _pods;


        protected override async Task OnInitializedAsync()
        {
            _nodes = NodeService.List();
            _pods  =  PodService.List();
        }

        public async Task OpenComponent(V1Node node)
        {

            await PageDrawerHelper<V1Node>.Instance
                .SetDrawerService(DrawerService)
                .ShowDrawerAsync<NodeDetailView, V1Node, bool>(node);
        }
    }
}
