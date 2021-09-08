using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Container
{
    public partial class ContainerStatus : ComponentBase
    {
        [Parameter]
        public V1Pod Pod { get; set; }
    }
}
