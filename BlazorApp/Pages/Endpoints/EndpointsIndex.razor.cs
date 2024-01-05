using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Endpoints;

public partial class EndpointsIndex : TableBase<V1Endpoints>
{
    [Inject]
    private IEndpointsService EndpointsService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1Endpoints> data)
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

    private async Task OnItemNameClick(V1Endpoints item)
    {
        await PageDrawerHelper<V1Endpoints>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<EndpointsDetailView, V1Endpoints, bool>(item);
    }

    private IList<string> GetEndPointsIpPorts(V1Endpoints ep)
    {
        var ret = new List<string>();
        if (ep.Subsets is null) return ret;

        ep.Subsets.Where(x => x.Ports is { Count: > 0 } && x.Addresses is { Count: > 0 })
            .ToList()
            .ForEach(x =>
            {
                x.Ports.ToList()
                    .ForEach(p => { ret.AddRange(x.Addresses.Select(m => $"{m.Ip}:{p.Port}/{p.Protocol}")); });
            });
        return ret;
    }
}
