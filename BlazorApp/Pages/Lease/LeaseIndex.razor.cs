using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Lease;

public partial class LeaseIndex : TableBase<V1Lease>
{
    [Inject] private ILeaseService LeaseService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1Lease> data)
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

    private async Task OnItemNameClick(V1Lease item)
    {
        await PageDrawerHelper<V1Lease>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<LeaseDetailView, V1Lease, bool>(item);
    }
}