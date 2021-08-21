using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using Entity;
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
        private IPodService PodService { get; set; }

        [Inject]
        private IEventService EventService { get; set; }
        private V1NodeList _nodes;
        private V1PodList _pods;
        public V1beta1EventList events;
        [Inject]
        private DrawerService DrawerService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            _nodes = await NodeService.List();
            _pods  = await PodService.List();
            events = await EventService.List();
        }

        public async Task OpenComponent(V1Node node, IList<V1Pod> pods)
        {
            var options = new DrawerOptions
            {
                Title ="Node:" + node.Name(),
                Width = 800
            };


            var drawerRef =
                await DrawerService.CreateAsync<NodeDetailView, NodeVO, bool>(options,
                    new NodeVO { Node = node, Pods = pods });
            // drawerRef.OnClosed = async result =>
            // {
            //     Console.WriteLine("OnAfterClosed:" + result.Name());
            //     await InvokeAsync(StateHasChanged);
            // };
        }
    }
}
