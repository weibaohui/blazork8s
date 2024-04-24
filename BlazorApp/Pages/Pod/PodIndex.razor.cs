using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Pages.Node;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod;

public partial class PodIndex : TableBase<V1Pod>
{
    private bool _metricsServerReady;
    private bool _showContainer;
    private string _sortBy = "";
    [Inject] private IMetricsService MetricsService { get; set; }

    [Inject] private IPodService PodService { get; set; }

    [Inject] private INodeService NodeService { get; set; }

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
        _metricsServerReady = await MetricsService.MetricsServerReady();
    }


    private async Task OnNodeNameClick(string nodeName)
    {
        var node = NodeService.GetByName(nodeName);
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

    private async Task OnTopClick()
    {
        var command = " top pods ";
        if (_selectedNs != "")
            command += $" -n {_selectedNs}";
        else
            command += " -A";

        if (_sortBy != "") command += $" --sort-by={_sortBy}";

        if (_showContainer) command += " --containers";

        var options = PageDrawerService.DefaultOptions("top pods", 1400);
        await PageDrawerService.ShowDrawerAsync<KubectlCommand, string, bool>(options, command);
    }


    private void OnSortByChanged(string args)
    {
        _sortBy = args;
    }
}
