using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.MutatingWebhookConfiguration
{
    public partial class MutatingWebhookConfigurationDetailView : FeedbackComponent<V1MutatingWebhookConfiguration, bool>
    {
        private V1MutatingWebhookConfiguration Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
