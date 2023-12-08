using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ConfigMap;

public partial class ConfigMapIndex : TableBase<V1ConfigMap>
{
    [Inject]
    private IConfigMapService ConfigMapService { get; set; }


    private async Task OnResourceChanged(ResourceCache<V1ConfigMap> data)
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


    private async Task OnItemNameClick(V1ConfigMap item)
    {
        await PageDrawerHelper<V1ConfigMap>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<ConfigMapDetailView, V1ConfigMap, bool>(item);

    }
}
