using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.ReferenceGrant;

public partial class ReferenceGrantIndex : TableBase<V1Alpha2ReferenceGrant>
{
    [Inject] private IReferenceGrantService ReferenceGrantService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1Alpha2ReferenceGrant> data)
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

    private async Task OnItemNameClick(V1Alpha2ReferenceGrant item)
    {
        await PageDrawerHelper<V1Alpha2ReferenceGrant>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<ReferenceGrantDetailView, V1Alpha2ReferenceGrant, bool>(item);
    }
}