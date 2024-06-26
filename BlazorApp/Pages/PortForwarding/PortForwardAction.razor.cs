using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Utils.PortForwarding;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.PortForwarding;

public partial class PortForwardAction : PageBase
{
    [Parameter] public PortForward Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private void OnDeleteClick(PortForward item)
    {
        PortForwardExecutorHelper.Instance.DisposeByItem(item);
        StateHasChanged();
    }


    private async Task OnYamlClick(PortForward item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<PortForward>, PortForward, bool>(options, item);
    }
}
