using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.HttpRoute;

public partial class HttpRouteIndex : TableBase<V1HTTPRoute>
{
    [Inject] private IHttpRouteService HttpRouteService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1HTTPRoute> data)
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

    private async Task OnItemNameClick(V1HTTPRoute item)
    {
        await PageDrawerHelper<V1HTTPRoute>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<HttpRouteDetailView, V1HTTPRoute, bool>(item);
    }
}