using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Node;

public partial class NodeAction : ComponentBase
{
    [Inject]
    IMessageService MessageService { get; set; }

    [Parameter]
    public V1Node Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; } = MenuMode.Vertical;

    [Inject]
    private INodeService NodeService { get; set; }

    private async Task OnNodeDeleteClick(V1Node item)
    {
        await NodeService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }


    private async Task OnCordonClick(V1Node item)
    {
        await NodeService.Cordon(item.Name());
       await MessageService.Success("Cordon Success");
    }
    private async Task OnUnCordonClick(V1Node item)
    {
        await NodeService.UnCordon(item.Name());
        await MessageService.Success("UnCordon Success");

    }
}
