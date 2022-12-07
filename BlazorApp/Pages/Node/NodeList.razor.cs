using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Service;
using Entity;
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

        [Inject]
        private IPageDrawerService PageDrawerService { get; set; }

        private V1NodeList _nodes;
        private V1PodList  _pods;


        protected override async Task OnInitializedAsync()
        {
            _nodes = await KubeService.Client().ListNodeAsync();
            _pods  = await KubeService.Client().ListPodForAllNamespacesAsync();
        }

        public async Task OpenComponent(V1Node node, IList<V1Pod> pods)
        {
            var options = PageDrawerService.DefaultOptions($"{node.Kind ?? "Node"}:{node.Name()}");
            await PageDrawerService.ShowDrawerAsync<NodeDetailView, NodeVO, bool>(options,
                new NodeVO { Node = node, Pods = pods });
        }
    }
}
