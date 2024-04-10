using BlazorApp.Pages.Common;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod;

public partial class PodTemplateView : PageBase
{
    [Parameter]
    public V1PodTemplateSpec PodTemplate { get; set; }

    [Parameter]
    public string MetadataExplainFieldPrefix { get; set; } = "Pod";
}
