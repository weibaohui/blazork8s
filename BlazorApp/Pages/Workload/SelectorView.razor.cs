using System.Collections.Generic;
using AntDesign;
using Microsoft.AspNetCore.Components;

namespace  BlazorApp.Pages.Workload
{
    public partial class SelectorView :ComponentBase
    {
        [Parameter]
        public IDictionary<string, string> Selector { get; set; }

        [Parameter]
        public string Title { get; set; }
    }
}
