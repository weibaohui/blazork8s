using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Job;
public partial class JobAction : ComponentBase
{
    [Parameter]
    public V1Job Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IJobService JobService { get; set; }

    private async Task OnDeleteClick(V1Job item)
    {
        await JobService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
