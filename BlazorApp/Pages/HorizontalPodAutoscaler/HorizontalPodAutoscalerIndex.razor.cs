using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Mapster;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.HorizontalPodAutoscaler;
public partial class HorizontalPodAutoscalerIndex : TableBase<V2HorizontalPodAutoscaler>
{
    [Inject]
    private IHorizontalPodAutoscalerService HorizontalPodAutoscalerService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V2HorizontalPodAutoscaler> data)
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
    private async Task OnItemNameClick(V2HorizontalPodAutoscaler item)
    {
        await PageDrawerHelper<V2HorizontalPodAutoscaler>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<HorizontalPodAutoscalerDetailView, V2HorizontalPodAutoscaler, bool>(item);
    }
    private V1ObjectReference TransferToRef(V2HorizontalPodAutoscaler hpa)
    {
        var _ref = hpa.Spec.ScaleTargetRef.Adapt<V1ObjectReference>();
        _ref.NamespaceProperty = hpa.Metadata.NamespaceProperty;
        return _ref;
    }
}
