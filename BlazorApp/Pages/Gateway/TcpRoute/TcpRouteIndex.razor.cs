using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.TcpRoute;

public partial class TcpRouteIndex : TableBase<V1Alpha2TCPRoute>
{
    [Inject] private ITcpRouteService TcpRouteService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1Alpha2TCPRoute> data)
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

    private async Task OnItemNameClick(V1Alpha2TCPRoute item)
    {
        await PageDrawerHelper<V1Alpha2TCPRoute>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<TcpRouteDetailView, V1Alpha2TCPRoute, bool>(item);
    }
}