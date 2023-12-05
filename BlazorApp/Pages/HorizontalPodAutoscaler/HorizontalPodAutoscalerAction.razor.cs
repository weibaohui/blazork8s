using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.HorizontalPodAutoscaler;

public partial class HorizontalPodAutoscalerAction : ComponentBase
{
    [Parameter]
    public V1HorizontalPodAutoscaler Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

    [Inject]
    private IHorizontalPodAutoscalerService HorizontalPodAutoscalerService { get; set; }


    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    [Inject]
    private ILogger<HorizontalPodAutoscalerAction> Logger { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task OnHorizontalPodAutoscalerDeleteClick(V1HorizontalPodAutoscaler item)
    {
        await HorizontalPodAutoscalerService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

 

    private async Task OnYamlClick(V1HorizontalPodAutoscaler item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1HorizontalPodAutoscaler>, V1HorizontalPodAutoscaler, bool>(options, item);
    }
}
