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

namespace BlazorApp.Pages.ReplicationController;

public partial class ReplicationControllerAction : ComponentBase
{
    [Parameter]
    public V1ReplicationController Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

    [Inject]
    private IReplicationControllerService ReplicationControllerService { get; set; }


    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task OnDeleteClick(V1ReplicationController item)
    {
        await ReplicationControllerService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

 

    private async Task OnYamlClick(V1ReplicationController item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1ReplicationController>, V1ReplicationController, bool>(options, item);
    }

    private async Task OnDocClick(V1ReplicationController item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<V1ReplicationController>, V1ReplicationController, bool>(options, item);
    }
}