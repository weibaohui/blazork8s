using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Common.Metadata;

public partial class ControllerBy : PageBase
{
     private IList<V1ObjectReference> Refs { get; set; }

    [Parameter]
    public bool ShowOwnerName { get; set; }

    [Parameter]
    public string Namespace { get; set; }

    [Parameter]
    public IList<V1OwnerReference> Owner { get; set; }

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
