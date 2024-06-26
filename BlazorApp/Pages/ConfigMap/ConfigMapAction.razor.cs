using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ConfigMap;

public partial class ConfigMapAction : PageBase
{
    [Parameter] public V1ConfigMap Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;

    [Inject] private IConfigMapService ConfigMapService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task OnYamlClick(V1ConfigMap item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1ConfigMap>, V1ConfigMap, bool>(options, item);
    }

    private async Task OnDocClick(V1ConfigMap item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<V1ConfigMap>, V1ConfigMap, bool>(options, item);
    }
}
