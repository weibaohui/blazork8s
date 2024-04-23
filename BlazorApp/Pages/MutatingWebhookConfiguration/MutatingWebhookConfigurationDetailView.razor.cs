using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.MutatingWebhookConfiguration;

public partial class MutatingWebhookConfigurationDetailView : DrawerPageBase<V1MutatingWebhookConfiguration>
{
    private V1MutatingWebhookConfiguration MutatingWebhookConfiguration { get; set; }

    protected override async Task OnInitializedAsync()
    {
        MutatingWebhookConfiguration = base.Options;
        await base.OnInitializedAsync();
    }
}