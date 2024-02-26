using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.PodDisruptionBudget;
public partial class PodDisruptionBudgetAction : ComponentBase
{
    [Parameter]
    public V1PodDisruptionBudget Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IPodDisruptionBudgetService PodDisruptionBudgetService { get; set; }

    private async Task OnDeleteClick(V1PodDisruptionBudget item)
    {
        await PodDisruptionBudgetService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
