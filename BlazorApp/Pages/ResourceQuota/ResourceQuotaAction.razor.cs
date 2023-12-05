using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.ResourceQuota;

public partial class ResourceQuotaAction : ComponentBase
{
    [Parameter]
    public V1ResourceQuota Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

    [Inject]
    private IResourceQuotaService ResourceQuotaService { get; set; }


    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    [Inject]
    private ILogger<ResourceQuotaAction> Logger { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task OnResourceQuotaDeleteClick(V1ResourceQuota item)
    {
        await ResourceQuotaService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

 

    private async Task OnYamlClick(V1ResourceQuota item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1ResourceQuota>, V1ResourceQuota, bool>(options, item);
    }
}
