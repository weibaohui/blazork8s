using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
namespace BlazorApp.Pages.ResourceQuota;
public partial class ResourceQuotaIndex : TableBase<V1ResourceQuota>
{
    [Inject]
    private IResourceQuotaService ResourceQuotaService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1ResourceQuota> data)
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
    private async Task OnItemNameClick(V1ResourceQuota item)
    {
        await PageDrawerHelper<V1ResourceQuota>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<ResourceQuotaDetailView, V1ResourceQuota, bool>(item);
    }
}