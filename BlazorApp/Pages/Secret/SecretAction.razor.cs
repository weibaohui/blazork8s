using System;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Pages.Workload;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
namespace BlazorApp.Pages.Secret;
public partial class SecretAction : ComponentBase
{
    [Parameter]
    public V1Secret Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private ISecretService SecretService { get; set; }
    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
    private async Task OnDeleteClick(V1Secret item)
    {
        await SecretService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }
    private async Task OnYamlClick(V1Secret item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1Secret>, V1Secret, bool>(options, item);
    }
    private async Task OnDocClick(V1Secret item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<V1Secret>, V1Secret, bool>(options, item);
    }
}