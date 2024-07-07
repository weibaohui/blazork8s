using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.GatewayClass;

public partial class GatewayClassIndex : TableBase<V1GatewayClass>
{
    [Inject] private IGatewayClassService GatewayClassService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1GatewayClass> data)
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

    private async Task OnItemNameClick(V1GatewayClass item)
    {
        await PageDrawerHelper<V1GatewayClass>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<GatewayClassDetailView, V1GatewayClass, bool>(item);
    }
}