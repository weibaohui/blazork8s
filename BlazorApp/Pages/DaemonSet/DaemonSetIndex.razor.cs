using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
namespace BlazorApp.Pages.DaemonSet;
public partial class DaemonSetIndex : TableBase<V1DaemonSet>
{
    [Inject]
    private IDaemonSetService DaemonSetService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1DaemonSet> data)
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
    private async Task OnItemNameClick(V1DaemonSet item)
    {
        await PageDrawerHelper<V1DaemonSet>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<DaemonSetDetailView, V1DaemonSet, bool>(item);
    }
}