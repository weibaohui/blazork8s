using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service;
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
    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
    private async Task OnDeleteClick(V1Namespace item)
    {
        await NamespaceService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }
    private async Task OnYamlClick(V1Namespace item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1Namespace>, V1Namespace, bool>(options, item);
    }
    private async Task OnDocClick(V1Namespace item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<V1Namespace>, V1Namespace, bool>(options, item);
    }
}