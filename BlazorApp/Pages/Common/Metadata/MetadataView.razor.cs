using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class MetadataView : ComponentBase
{
    [Parameter]
    public V1ObjectMeta Item { get; set; }

    [Parameter]
    public string ExplainFieldPrefix { get; set; } = "Pod";
}
