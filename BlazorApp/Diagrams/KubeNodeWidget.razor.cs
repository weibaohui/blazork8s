using BlazorApp.Pages.Common;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Diagrams;

public partial class KubeNodeWidget<T> : PageBase where T : IKubernetesObject<V1ObjectMeta>
{
    [Parameter] public KubeNode<T> Node { get; set; }
}
