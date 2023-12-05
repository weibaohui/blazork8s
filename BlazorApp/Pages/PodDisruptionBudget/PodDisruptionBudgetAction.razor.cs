using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.PodDisruptionBudget;

public partial class PodDisruptionBudgetAction : ComponentBase
{
    [Parameter]
    public V1PodDisruptionBudget Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

    [Inject]
    private IPodDisruptionBudgetService PodDisruptionBudgetService { get; set; }


    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    [Inject]
    private ILogger<PodDisruptionBudgetAction> Logger { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task OnPodDisruptionBudgetDeleteClick(V1PodDisruptionBudget item)
    {
        await PodDisruptionBudgetService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

 

    private async Task OnYamlClick(V1PodDisruptionBudget item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1PodDisruptionBudget>, V1PodDisruptionBudget, bool>(options, item);
    }
}
