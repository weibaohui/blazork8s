using System.Collections.Generic;
using AntDesign;
using Microsoft.AspNetCore.Components;

namespace  BlazorApp.Pages.Workload
{
    public partial class LabelsView :ComponentBase
    {
        [Parameter]
        public IDictionary<string, string> Labels { get; set; }
    }
}
