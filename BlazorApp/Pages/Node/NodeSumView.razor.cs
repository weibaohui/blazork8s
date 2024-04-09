using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Node
{
    public partial class NodeSumView : PageBase
    {
        [Parameter]
        public V1Node Node { get; set; }
    }
}
