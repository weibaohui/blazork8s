using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class AnnotationsView : PageBase
{
    [Parameter] public IDictionary<string, string> Annotations { get; set; }

    [Parameter] public string ExplainField { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Annotations != null)
        {
            Annotations.Remove("kubectl.kubernetes.io/last-applied-configuration");
        }

        await base.OnInitializedAsync();
    }
}