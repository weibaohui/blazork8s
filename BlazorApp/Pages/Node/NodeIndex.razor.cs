using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Node;

public partial class NodeIndex : TableBase<V1Node>
{
    [Inject]
    private INodeService NodeService { get; set; }


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
}
