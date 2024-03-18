using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class FinalizersView : ComponentBase
{
    [Parameter]
    public IList<string> Finalizers { get; set; }

    [Parameter]
    public string ExplainField { get; set; }
}
