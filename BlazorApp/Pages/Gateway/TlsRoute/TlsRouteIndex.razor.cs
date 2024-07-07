using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.TlsRoute;

public partial class TlsRouteIndex : TableBase<V1Alpha2TLSRoute>
{
    [Inject] private ITlsRouteService TlsRouteService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1Alpha2TLSRoute> data)
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

    private async Task OnItemNameClick(V1Alpha2TLSRoute item)
    {
        await PageDrawerHelper<V1Alpha2TLSRoute>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<TlsRouteDetailView, V1Alpha2TLSRoute, bool>(item);
    }
}