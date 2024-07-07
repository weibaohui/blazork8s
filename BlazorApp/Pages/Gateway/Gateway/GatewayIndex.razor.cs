using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.Gateway;

public partial class GatewayIndex : TableBase<V1Gateway>
{
    [Inject] private IGatewayService GatewayService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1Gateway> data)
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

    private async Task OnItemNameClick(V1Gateway item)
    {
        await PageDrawerHelper<V1Gateway>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<GatewayDetailView, V1Gateway, bool>(item);
    }
}