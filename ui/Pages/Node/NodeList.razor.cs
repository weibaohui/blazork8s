using System;
using System.Threading.Tasks;
using AntDesign;
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

        [Inject]
        private DrawerService DrawerService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            _nodes = await NodeService.List();
        }

        public async Task OpenComponent(V1Node node)
        {
            Console.WriteLine(node.Name());
            var options = new DrawerOptions
            {
                Title = node.Name(),
                Width = 600
            };

            var drawerRef = await DrawerService.CreateAsync<NodeDetailView, V1Node, V1Node>(options, node);
            drawerRef.OnClosed = async result =>
            {
                Console.WriteLine("OnAfterClosed:" + result.Name());
                await InvokeAsync(StateHasChanged);
            };
        }
    }
}
