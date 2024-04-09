using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.PersistentVolumeClaim;
public partial class PersistentVolumeClaimIndex : TableBase<V1PersistentVolumeClaim>
{
    [Inject]
    private IPersistentVolumeClaimService PersistentVolumeClaimService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1PersistentVolumeClaim> data)
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
    private async Task OnItemNameClick(V1PersistentVolumeClaim item)
    {
        await PageDrawerHelper<V1PersistentVolumeClaim>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<PersistentVolumeClaimDetailView, V1PersistentVolumeClaim, bool>(item);
    }
}
