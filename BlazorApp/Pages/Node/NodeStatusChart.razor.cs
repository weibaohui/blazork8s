using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Pod;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Node;

public partial class NodeStatusChart : PageBase, IDisposable
{
    private Timer _timer;
    private IList<V1Pod> podsOnNode;

    [Parameter] public V1Node Node { get; set; }

    [Inject] public IPodService PodService { get; set; }

    public void Dispose()
    {
        _timer.Dispose();
    }

    protected override async Task OnInitializedAsync()
    {
        _timer = new Timer(3000);
        _timer.Elapsed += async (sender, eventArgs) => await OnTimerCallback();
        _timer.Start();
        podsOnNode = PodService.ListByNodeName(Node.Name());
        await base.OnInitializedAsync();
    }

    private async Task OnTimerCallback()
    {
        podsOnNode = PodService.ListByNodeName(Node.Name());
        await InvokeAsync(StateHasChanged);
    }

    private async Task OnPodClick(V1Pod pod)
    {
        await PageDrawerHelper<V1Pod>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<PodDetailView, V1Pod, bool>(pod);
    }

    private async Task OnNodeMetricsClick(V1Node item)
    {
        await PageDrawerHelper<V1Node>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<NodeMetricsDetailTabView, V1Node, bool>(item);
    }
}
