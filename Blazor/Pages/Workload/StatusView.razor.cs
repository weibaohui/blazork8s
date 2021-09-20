using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Workload
{
    public partial class StatusView:ComponentBase
    {
        [Parameter]
        public string Status { get; set; }

    }
}
