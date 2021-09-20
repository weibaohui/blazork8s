using System.Collections.Generic;
using AntDesign;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Workload
{
    public partial class AnnotationsView :ComponentBase
    {
        [Parameter]
        public IDictionary<string, string> Annotations { get; set; }
    }
}
