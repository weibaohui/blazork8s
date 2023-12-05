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
}
