using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class SelectorView : PageBase
{
    [Parameter] public IDictionary<string, string> Selector { get; set; }

    [Parameter] public string Title { get; set; }

    [Parameter] public string ExplainField { get; set; } = "";
}
