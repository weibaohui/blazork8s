using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class ControllerByView : PageBase
{
    [Parameter] public IList<V1OwnerReference> Owner { get; set; }

    [Parameter] public string Namespace { get; set; }

    [Parameter] public string ExplainField { get; set; }

    private IList<V1ObjectReference> Refs { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Owner != null)
        {
            Refs = Owner.Select(x => new V1ObjectReference()
            {
                ApiVersion = x.ApiVersion,
                Kind = x.Kind,
                Name = x.Name,
                NamespaceProperty = Namespace
            }).ToList();
        }

        await base.OnInitializedAsync();
    }
}