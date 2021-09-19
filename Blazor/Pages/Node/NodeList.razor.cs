using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using Blazor.Service;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Node
{
    public partial class NodeList : ComponentBase
    {
        [Inject]
        private INodeService NodeService { get; set; }

        [Inject]
        private IPodService PodService { get; set; }

        private V1NodeList _nodes;
        private V1PodList  _pods;

        [Inject]
        private DrawerService DrawerService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            _nodes = await NodeService.List();
            _pods  = await PodService.List();
        }

        public async Task OpenComponent(V1Node node, IList<V1Pod> pods)
        {
            await NodeService.ShowNodeDrawer(node, pods);
        }
    }
}
