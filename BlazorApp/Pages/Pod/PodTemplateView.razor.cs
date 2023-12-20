using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod;

public partial class PodTemplateView:ComponentBase
{
    [Parameter]
    public V1PodTemplateSpec PodTemplateItem { get; set; }

    [Parameter]
    public string MetadataExplainFieldPrefix { get; set; } = "Pod";
}
