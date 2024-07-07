using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.GrpcRoute;

public partial class GrpcRouteIndex : TableBase<V1GRPCRoute>
{
    [Inject] private IGrpcRouteService GrpcRouteService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1GRPCRoute> data)
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

    private async Task OnItemNameClick(V1GRPCRoute item)
    {
        await PageDrawerHelper<V1GRPCRoute>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<GrpcRouteDetailView, V1GRPCRoute, bool>(item);
    }
}