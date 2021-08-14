using System.Threading.Tasks;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using ui.Service;

namespace ui.Pages.Node
{
    public partial class NodeList : ComponentBase
    {
        [Inject]
        private INodeService NodeService { get; set; }

        [Inject]
        private INodeApi _nodeApi { get; set; }

        [Inject]
        private IPodService PodService { get; set; }

        private V1NodeList _nodes;
        private V1PodList  _pods;

        protected override async Task OnInitializedAsync()
        {
            _nodes = await NodeService.List();
            _pods  = await PodService.List();
        }
    }
}
