using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Secret;
public partial class SecretAction : ComponentBase
{
    [Parameter]
    public V1Secret Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private ISecretService SecretService { get; set; }


    private async Task OnDeleteClick(V1Secret item)
    {
        await SecretService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
