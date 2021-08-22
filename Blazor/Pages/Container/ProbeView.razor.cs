using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Container
{
    public partial class ProbeView : ComponentBase
    {
        [Parameter]
        public V1Probe Probe { get; set; }

        [Parameter]
        public string ProbeType { get; set; }
    }
}
