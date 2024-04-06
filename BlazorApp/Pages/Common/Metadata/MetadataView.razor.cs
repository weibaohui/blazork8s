using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BlazorApp.Pages.Common.Metadata;

public partial class MetadataView : ComponentBase
{
    [Inject]
    public IStringLocalizer L { get; set; }

    [Parameter]
    public V1ObjectMeta Item { get; set; }

    [Parameter]
    public string ExplainFieldPrefix { get; set; } = "Pod";

    private V1ObjectReference NsRef(string func)
    {
        return new V1ObjectReference()
        {
            Name = Item.Namespace(),
            Kind = "Namespace"
        };
    }
}