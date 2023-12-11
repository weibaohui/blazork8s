using System;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Pages.Workload;
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

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task OnDeleteClick(V1HorizontalPodAutoscaler item)
    {
        await HorizontalPodAutoscalerService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

 

    private async Task OnYamlClick(V1HorizontalPodAutoscaler item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1HorizontalPodAutoscaler>, V1HorizontalPodAutoscaler, bool>(options, item);
    }

    private async Task OnDocClick(V1HorizontalPodAutoscaler item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<V1HorizontalPodAutoscaler>, V1HorizontalPodAutoscaler, bool>(options, item);
    }
}
