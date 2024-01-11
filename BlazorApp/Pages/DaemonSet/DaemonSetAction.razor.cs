using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service;
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

    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task OnDeleteClick(V1DaemonSet item)
    {
        await DaemonSetService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

    private async Task OnYamlClick(V1DaemonSet item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1DaemonSet>, V1DaemonSet, bool>(options, item);
    }

    private async Task OnDocClick(V1DaemonSet item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<V1DaemonSet>, V1DaemonSet, bool>(options, item);
    }

    private async Task OnRestartClick(V1DaemonSet item)
    {
        await DaemonSetService.Restart(item);
        await MessageService.Success($"{item.Name()} Restarted");
    }
}
