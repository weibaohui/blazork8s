using System.Collections.Generic;
using AntDesign;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Workload
{
    public partial class LabelsView :ComponentBase
    {
        [Parameter]
        public IDictionary<string, string> Labels { get; set; }
    }
}
