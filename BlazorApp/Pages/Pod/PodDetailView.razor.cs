using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Node;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod
{
    public partial class PodDetailView : DrawerPageBase<V1Pod>
    {
        [Inject]
        private INodeService NodeService { get; set; }

        [Inject]
        private DrawerService DrawerService { get; set; }

        private V1Pod Item;

        protected override void OnInitialized()
        {
            Item = base.Options;
            base.OnInitialized();
        }

        private async Task OnNodeNameClick(string nodeName)
        {
            var node    = NodeService.GetByName(nodeName);
            await PageDrawerHelper<V1Node>.Instance
                .SetDrawerService(DrawerService)
                .ShowDrawerAsync<NodeDetailView, V1Node, bool>(node);
        }
    }
}
