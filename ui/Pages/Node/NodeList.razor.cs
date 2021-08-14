using System.Threading.Tasks;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using ui.Service;

namespace ui.Pages.Node
{
    public partial class NodeList : ComponentBase
    {
        private V1NodeList _nodes;

        [Inject]
        private INodeService NodeService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            _nodes = await NodeService.List();
        }
    }
}
