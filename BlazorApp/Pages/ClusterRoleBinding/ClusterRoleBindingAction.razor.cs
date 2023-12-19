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
namespace BlazorApp.Pages.ClusterRoleBinding;
public partial class ClusterRoleBindingAction : ComponentBase
{
    [Parameter]
    public V1ClusterRoleBinding Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IClusterRoleBindingService ClusterRoleBindingService { get; set; }
    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
    private async Task OnDeleteClick(V1ClusterRoleBinding item)
    {
        await ClusterRoleBindingService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }
    private async Task OnYamlClick(V1ClusterRoleBinding item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1ClusterRoleBinding>, V1ClusterRoleBinding, bool>(options, item);
    }
    private async Task OnDocClick(V1ClusterRoleBinding item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<V1ClusterRoleBinding>, V1ClusterRoleBinding, bool>(options, item);
    }
}