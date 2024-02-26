using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ServiceAccount;
public partial class ServiceAccountAction : ComponentBase
{
    [Parameter]
    public V1ServiceAccount Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IServiceAccountService ServiceAccountService { get; set; }


    private async Task OnDeleteClick(V1ServiceAccount item)
    {
        await ServiceAccountService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
