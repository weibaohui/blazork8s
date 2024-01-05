using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.EndpointSlice;

public partial class EndpointSliceIndex : TableBase<V1EndpointSlice>
{
    [Inject]
    private IEndpointSliceService EndpointSliceService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1EndpointSlice> data)
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

    private async Task OnItemNameClick(V1EndpointSlice item)
    {
        await PageDrawerHelper<V1EndpointSlice>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<EndpointSliceDetailView, V1EndpointSlice, bool>(item);
    }

    private IList<string> GetEndPointsIpPorts(V1EndpointSlice es)
    {
        var ret = new List<string>();
        if (es.Endpoints is null) return ret;
        if (es.Ports is not { Count: > 0 }) return ret;
        foreach (var port in es.Ports)
        {
            es.Endpoints
                .Where(x => x.Addresses is { Count: > 0 })
                .ToList()
                .SelectMany(x => x.Addresses)
                .ToList()
                .ForEach(x => ret.Add($"{x}:{port.Port}/{port.Protocol}"));
        }

        return ret;
    }
}
