using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.HorizontalPodAutoscaler;

public partial class HorizontalPodAutoscalerIndex : TableBase<V1HorizontalPodAutoscaler>
{
    [Inject]
    private IHorizontalPodAutoscalerService HorizontalPodAutoscalerService { get; set; }


    private async Task OnResourceChanged(ResourceCache<V1HorizontalPodAutoscaler> data)
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


    private async Task OnItemNameClick(V1HorizontalPodAutoscaler item)
    {
        await PageDrawerHelper<V1HorizontalPodAutoscaler>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<HorizontalPodAutoscalerDetailView, V1HorizontalPodAutoscaler, bool>(item);

    }
}
