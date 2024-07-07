using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.UdpRoute;

public partial class UdpRouteIndex : TableBase<V1Alpha2UDPRoute>
{
    [Inject] private IUdpRouteService UdpRouteService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1Alpha2UDPRoute> data)
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

    private async Task OnItemNameClick(V1Alpha2UDPRoute item)
    {
        await PageDrawerHelper<V1Alpha2UDPRoute>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<UdpRouteDetailView, V1Alpha2UDPRoute, bool>(item);
    }
}