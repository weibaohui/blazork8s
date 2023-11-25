using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.metadata;

public partial class AnnotationsView : ComponentBase
{
    [Parameter]
    public IDictionary<string, string> Annotations { get; set; }
}