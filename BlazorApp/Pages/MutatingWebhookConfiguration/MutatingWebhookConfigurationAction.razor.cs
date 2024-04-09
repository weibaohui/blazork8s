using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.MutatingWebhookConfiguration;

public partial class MutatingWebhookConfigurationAction : PageBase
{
    [Parameter]
    public V1MutatingWebhookConfiguration Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;


}
