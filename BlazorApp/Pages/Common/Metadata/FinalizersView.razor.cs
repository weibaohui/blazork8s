using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BlazorApp.Pages.Common.Metadata;

public partial class FinalizersView : ComponentBase
{
    [Inject]
    public IStringLocalizer L { get; set; }

    [Parameter]
    public IList<string> Finalizers { get; set; }

    [Parameter]
    public string ExplainField { get; set; }
}