using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.CronJob;
public partial class CronJobAction : ComponentBase
{
    [Parameter]
    public V1CronJob Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private ICronJobService CronJobService { get; set; }

    private async Task OnDeleteClick(V1CronJob item)
    {
        await CronJobService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
