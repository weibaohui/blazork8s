using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace  BlazorApp.Pages.Container
{
    public partial class ContainerStatus : PageBase
    {
        [Parameter]
        public V1Pod Pod { get; set; }
    }
}
