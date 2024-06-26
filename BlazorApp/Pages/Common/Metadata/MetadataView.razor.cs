using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class MetadataView : PageBase
{
    [Parameter] public V1ObjectMeta Item { get; set; }

    [Parameter] public string ExplainFieldPrefix { get; set; } = "Pod";

    private V1ObjectReference NsRef(string func)
    {
        return KubeHelper.GetObjectRef("Namespace", Item.Namespace());
    }
}
