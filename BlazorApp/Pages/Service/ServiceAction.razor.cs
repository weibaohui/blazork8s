using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Service;

public partial class ServiceAction : ComponentBase
{
    [Parameter]
    public V1Service Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

    [Inject]
    private IServiceService ServiceService { get; set; }



    private async Task OnDeleteClick(V1Service item)
    {
        await ServiceService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

 


}
