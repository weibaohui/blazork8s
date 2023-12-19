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
namespace BlazorApp.Pages.PersistentVolumeClaim;
public partial class PersistentVolumeClaimAction : ComponentBase
{
    [Parameter]
    public V1PersistentVolumeClaim Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IPersistentVolumeClaimService PersistentVolumeClaimService { get; set; }
    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
    private async Task OnDeleteClick(V1PersistentVolumeClaim item)
    {
        await PersistentVolumeClaimService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }
    private async Task OnYamlClick(V1PersistentVolumeClaim item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1PersistentVolumeClaim>, V1PersistentVolumeClaim, bool>(options, item);
    }
    private async Task OnDocClick(V1PersistentVolumeClaim item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<V1PersistentVolumeClaim>, V1PersistentVolumeClaim, bool>(options, item);
    }
}