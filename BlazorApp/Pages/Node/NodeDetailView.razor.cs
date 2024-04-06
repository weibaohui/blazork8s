using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Node;

public partial class NodeDetailView : DrawerPageBase<V1Node>
{
    private bool         _isMetricsServerReady;
    private IList<V1Pod> _pods;
    public  V1Node       Node;

    [Inject]
    private IMetricsService MetricsService { get; set; }

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