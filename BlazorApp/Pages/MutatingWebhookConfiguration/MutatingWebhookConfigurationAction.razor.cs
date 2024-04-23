using AntDesign;
using BlazorApp.Pages.Common;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.MutatingWebhookConfiguration;

public partial class MutatingWebhookConfigurationAction : PageBase
{
    [Parameter] public V1MutatingWebhookConfiguration Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;
}