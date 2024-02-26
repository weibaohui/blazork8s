using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.EndpointSlice;
public partial class EndpointSliceAction : ComponentBase
{
    [Parameter]
    public V1EndpointSlice Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IEndpointSliceService EndpointSliceService { get; set; }

    private async Task OnDeleteClick(V1EndpointSlice item)
    {
        await EndpointSliceService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
