using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Crd;
public partial class CrdAction : ComponentBase
{
    [Parameter]
    public V1CustomResourceDefinition Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private ICustomResourceDefinitionService CustomResourceDefinitionService { get; set; }

    private async Task OnDeleteClick(V1CustomResourceDefinition item)
    {
        await CustomResourceDefinitionService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }
}
