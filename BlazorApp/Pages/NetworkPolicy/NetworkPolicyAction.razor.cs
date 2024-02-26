using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.NetworkPolicy;
public partial class NetworkPolicyAction : ComponentBase
{
    [Parameter]
    public V1NetworkPolicy Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private INetworkPolicyService NetworkPolicyService { get; set; }

    private async Task OnDeleteClick(V1NetworkPolicy item)
    {
        await NetworkPolicyService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
