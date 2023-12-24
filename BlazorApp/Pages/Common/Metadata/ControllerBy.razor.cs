using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class ControllerBy : ComponentBase
{
    [Parameter]
    public IList<V1ObjectReference> Refs { get; set; }

    [Parameter]
    public bool ShowOwnerName { get; set; }

    [Parameter]
    public IList<V1OwnerReference> Owner { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Owner != null)
        {
            Refs = AutoMapperHelper<V1OwnerReference>.MapToObjectReference(Owner);
        }

        await base.OnInitializedAsync();
    }
}
