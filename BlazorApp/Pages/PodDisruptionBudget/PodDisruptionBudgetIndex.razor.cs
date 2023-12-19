using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
namespace BlazorApp.Pages.PodDisruptionBudget;
public partial class PodDisruptionBudgetIndex : TableBase<V1PodDisruptionBudget>
{
    [Inject]
    private IPodDisruptionBudgetService PodDisruptionBudgetService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1PodDisruptionBudget> data)
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
    private async Task OnItemNameClick(V1PodDisruptionBudget item)
    {
        await PageDrawerHelper<V1PodDisruptionBudget>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<PodDisruptionBudgetDetailView, V1PodDisruptionBudget, bool>(item);
    }
}