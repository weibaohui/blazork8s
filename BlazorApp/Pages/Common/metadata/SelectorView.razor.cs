using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.metadata;

public partial class SelectorView : ComponentBase
{
    [Parameter]
    public IDictionary<string, string> Selector { get; set; }

    [Parameter]
    public string Title { get; set; }
}