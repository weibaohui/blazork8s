using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Node
{
    public partial class NodeDetailView : FeedbackComponent<V1Node, bool>
    {
        [Inject]
        private IMetricsService MetricsService { get; set; }

        private bool         _isMetricsServerReady;
        public  V1Node       Node;
        private IList<V1Pod> _pods;

        [Inject]
        private IPodService PodService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Node                  = Options;
            _pods                 = PodService.ListByNodeName(Node.Name());
            _isMetricsServerReady = await MetricsService.MetricsServerReady();
            await base.OnInitializedAsync();
        }

        private async Task OnResourceChanged(ResourceCache<V1Pod> data)
        {
            await InvokeAsync(StateHasChanged);
        }

        private async void OnClose()
        {
            var drawerRef = FeedbackRef as DrawerRef<bool>;
            await drawerRef!.CloseAsync(true);
        }
    }
}
