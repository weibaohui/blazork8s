using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.Service;

public partial class ServiceAction : ComponentBase
{
    [Parameter]
    public V1Service Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

    [Inject]
    private IServiceService ServiceService { get; set; }


    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    [Inject]
    private ILogger<ServiceAction> Logger { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task OnServiceDeleteClick(V1Service item)
    {
        await ServiceService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

 

    private async Task OnYamlClick(V1Service item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1Service>, V1Service, bool>(options, item);
    }
}
