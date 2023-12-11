using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

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
