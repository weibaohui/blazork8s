using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Node;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod
{
    public partial class PodIndex : TableBase<V1Pod>
    {
        [Inject]
        private IPodService PodService { get; set; }

        [Inject]
        private INodeService NodeService { get; set; }

        private async Task OnResourceChanged(ResourceCache<V1Pod> data)
        {
            ItemList = data;
            TableData.CopyData(ItemList);
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            TableData.CopyData(ItemList);
            await InvokeAsync(StateHasChanged);
        }


        private async Task OnNodeNameClick(string nodeName)
        {
            var node    = NodeService.GetByName(nodeName);
            await PageDrawerHelper<V1Node>.Instance
                .SetDrawerService(PageDrawerService.DrawerService)
                .ShowDrawerAsync<NodeDetailView, V1Node, bool>(node);
        }

        private async Task OnPodClick(V1Pod pod)
        {
            await PageDrawerHelper<V1Pod>.Instance
                .SetDrawerService(PageDrawerService.DrawerService)
                .ShowDrawerAsync<PodDetailView, V1Pod, bool>(pod);
        }
    }
}
