using Microsoft.AspNetCore.Components;

namespace  BlazorApp.Pages.Workload
{
    public partial class StatusView:ComponentBase
    {
        [Parameter]
        public string Status { get; set; }

    }
}
