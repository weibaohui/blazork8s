using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.DaemonSet;

public partial class DaemonSetAction : PageBase
{
    [Parameter]
    public V1DaemonSet Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; } = MenuMode.Vertical;

    [Inject]
    IMessageService MessageService { get; set; }

    [Inject]
    private IDaemonSetService DaemonSetService { get; set; }



    private async Task OnRestartClick(V1DaemonSet item)
    {
        await DaemonSetService.Restart(item);
        await MessageService.Success($"{item.Name()} Restarted");
    }
}
