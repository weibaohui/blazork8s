using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.HorizontalPodAutoscaler;
public partial class HorizontalPodAutoscalerAction : ComponentBase
{
    [Parameter]
    public V1HorizontalPodAutoscaler Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IHorizontalPodAutoscalerService HorizontalPodAutoscalerService { get; set; }

    private async Task OnDeleteClick(V1HorizontalPodAutoscaler item)
    {
        await HorizontalPodAutoscalerService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }
}
