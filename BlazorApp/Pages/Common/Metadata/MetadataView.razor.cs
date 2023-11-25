using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class MetadataView<T>:ComponentBase  where T : IKubernetesObject<V1ObjectMeta>
{
    [Parameter]
    public T Item { get; set; }

}
