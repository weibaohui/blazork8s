using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service;
using BlazorApp.Service.impl;
using k8s;
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

        private V1NodeList _nodes;
        private V1PodList  _pods;


        protected override async Task OnInitializedAsync()
        {
            _nodes = await KubeService.Client().ListNodeAsync();
            _pods  = await KubeService.Client().ListPodForAllNamespacesAsync();
        }

        public async Task OpenComponent(V1Node node, IList<V1Pod> pods)
        {
            await NodeService.ShowNodeDrawer(node, pods);
        }
    }
}
