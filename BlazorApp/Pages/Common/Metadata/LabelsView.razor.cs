using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class LabelsView : ComponentBase
{
    [Parameter]
    public IDictionary<string, string> Labels { get; set; }
}
