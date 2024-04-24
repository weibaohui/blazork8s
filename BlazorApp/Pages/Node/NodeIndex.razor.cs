using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Node;

public partial class NodeIndex : TableBase<V1Node>
{
    private bool _metricsServerReady;
    private string _sortBy = "";

    [Inject] private INodeService NodeService { get; set; }

    [Inject] private IPodService PodService { get; set; }

    [Inject] private IMetricsService MetricsService { get; set; }


    private async Task OnResourceChanged(ResourceCache<V1Node> data)
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


    private async Task OnItemNameClick(V1Node item)
    {
        await PageDrawerHelper<V1Node>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<NodeDetailView, V1Node, bool>(item);
    }

    private List<string> GetNodeRole(V1Node item)
    {
        return item.Labels()
            .Where(x => x.Key.Contains("node-role.kubernetes.io/"))
            .Select(x => x.Key.Split("/")[1])
            .ToList();
    }

    private async Task OnTopClick()
    {
        var command = " top nodes ";

        if (_sortBy != "") command += $" --sort-by={_sortBy}";


        var options = PageDrawerService.DefaultOptions("top pods", 1400);
        await PageDrawerService.ShowDrawerAsync<KubectlCommand, string, bool>(options, command);
    }


    private void OnSortByChanged(string args)
    {
        _sortBy = args;
    }
}
