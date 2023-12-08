using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.PersistentVolume;

public partial class PersistentVolumeIndex : TableBase<V1PersistentVolume>
{
    [Inject]
    private IPersistentVolumeService PersistentVolumeService { get; set; }


    private async Task OnResourceChanged(ResourceCache<V1PersistentVolume> data)
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


    private async Task OnItemNameClick(V1PersistentVolume item)
    {
        await PageDrawerHelper<V1PersistentVolume>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<PersistentVolumeDetailView, V1PersistentVolume, bool>(item);

    }
}
