using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
namespace BlazorApp.Pages.StatefulSet;
public partial class StatefulSetIndex : TableBase<V1StatefulSet>
{
    [Inject]
    private IStatefulSetService StatefulSetService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1StatefulSet> data)
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
    private async Task OnItemNameClick(V1StatefulSet item)
    {
        await PageDrawerHelper<V1StatefulSet>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<StatefulSetDetailView, V1StatefulSet, bool>(item);
    }
}