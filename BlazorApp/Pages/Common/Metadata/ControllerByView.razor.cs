using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class ControllerByView : ComponentBase
{
    [Parameter]
    public IList<V1OwnerReference> Owner { get; set; }
}