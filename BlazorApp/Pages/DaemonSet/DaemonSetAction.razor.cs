using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.DaemonSet;

public partial class DaemonSetAction : ComponentBase
{
    [Parameter]
    public V1DaemonSet Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; } = MenuMode.Vertical;

    [Inject]
    IMessageService MessageService { get; set; }

    [Inject]
    private IDaemonSetService DaemonSetService { get; set; }


    private async Task OnDeleteClick(V1DaemonSet item)
    {
        await DaemonSetService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }
    private async Task OnRestartClick(V1DaemonSet item)
    {
        await DaemonSetService.Restart(item);
        await MessageService.Success($"{item.Name()} Restarted");
    }
}
