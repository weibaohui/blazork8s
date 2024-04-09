using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.StorageClass;

public partial class StorageClassIndex : TableBase<V1StorageClass>
{
    [Inject]
    private IStorageClassService StorageClassService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1StorageClass> data)
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

    private async Task OnItemNameClick(V1StorageClass item)
    {
        await PageDrawerHelper<V1StorageClass>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<StorageClassDetailView, V1StorageClass, bool>(item);
    }
}
