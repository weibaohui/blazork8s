using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Node;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod;

public partial class PodSpecView : PageBase
{
    [Parameter]
    public V1PodSpec PodSpec { get; set; }
    [Inject]
    private INodeService NodeService { get; set; }
    protected override async Task OnInitializedAsync()
    {

        await base.OnInitializedAsync();
    }

    private async Task OnNodeNameClick(string nodeName)
    {
        var node = NodeService.GetByName(nodeName);
        await PageDrawerHelper<V1Node>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<NodeDetailView, V1Node, bool>(node);
    }
}
