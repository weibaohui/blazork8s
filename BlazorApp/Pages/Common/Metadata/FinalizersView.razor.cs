using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;
using Microsoft.Extensions.Localization;

namespace BlazorApp.Pages.Common.Metadata;

public partial class FinalizersView : PageBase
{


    [Parameter]
    public IList<string> Finalizers { get; set; }

    [Parameter]
    public string ExplainField { get; set; }
}
