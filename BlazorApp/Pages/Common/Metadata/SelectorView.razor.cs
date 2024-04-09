using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Common.Metadata;

public partial class SelectorView : PageBase
{
    [Parameter]
    public IDictionary<string, string> Selector { get; set; }

    [Parameter]
    public string Title { get; set; }
}
