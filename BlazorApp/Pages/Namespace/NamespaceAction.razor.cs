using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Namespace;
public partial class NamespaceAction : ComponentBase
{
    [Parameter]
    public V1Namespace Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private INamespaceService NamespaceService { get; set; }

    private async Task OnDeleteClick(V1Namespace item)
    {
        await NamespaceService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
