using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.LimitRange;
public partial class LimitRangeIndex : TableBase<V1LimitRange>
{
    [Inject]
    private ILimitRangeService LimitRangeService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1LimitRange> data)
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
    private async Task OnItemNameClick(V1LimitRange item)
    {
        await PageDrawerHelper<V1LimitRange>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<LimitRangeDetailView, V1LimitRange, bool>(item);
    }
}
