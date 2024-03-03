using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Lease;
public partial class LeaseAction : ComponentBase
{
    [Parameter]
    public V1Lease Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private ILeaseService LeaseService { get; set; }
    private async Task OnDeleteClick(V1Lease item)
    {
        await LeaseService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }
}