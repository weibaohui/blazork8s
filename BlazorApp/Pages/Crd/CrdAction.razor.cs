using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service;
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
    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
    private async Task OnDeleteClick(V1CustomResourceDefinition item)
    {
        await CustomResourceDefinitionService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }
    private async Task OnYamlClick(V1CustomResourceDefinition item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1CustomResourceDefinition>, V1CustomResourceDefinition, bool>(options, item);
    }
    private async Task OnDocClick(V1CustomResourceDefinition item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<V1CustomResourceDefinition>, V1CustomResourceDefinition, bool>(options, item);
    }
}
