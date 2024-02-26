using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.MutatingWebhookConfiguration;
public partial class MutatingWebhookConfigurationAction : ComponentBase
{
    [Parameter]
    public V1MutatingWebhookConfiguration Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IMutatingWebhookConfigurationService MutatingWebhookConfigurationService { get; set; }

    private async Task OnDeleteClick(V1MutatingWebhookConfiguration item)
    {
        await MutatingWebhookConfigurationService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
