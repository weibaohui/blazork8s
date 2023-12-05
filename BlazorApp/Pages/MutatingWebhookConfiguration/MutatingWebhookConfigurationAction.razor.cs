using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.MutatingWebhookConfiguration;

public partial class MutatingWebhookConfigurationAction : ComponentBase
{
    [Parameter]
    public V1MutatingWebhookConfiguration Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

    [Inject]
    private IMutatingWebhookConfigurationService MutatingWebhookConfigurationService { get; set; }


    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    [Inject]
    private ILogger<MutatingWebhookConfigurationAction> Logger { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task OnMutatingWebhookConfigurationDeleteClick(V1MutatingWebhookConfiguration item)
    {
        await MutatingWebhookConfigurationService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

 

    private async Task OnYamlClick(V1MutatingWebhookConfiguration item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1MutatingWebhookConfiguration>, V1MutatingWebhookConfiguration, bool>(options, item);
    }
}
