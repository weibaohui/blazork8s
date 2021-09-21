using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Workload
{
    public partial class ControllerByView: ComponentBase
    {
        [Parameter]
        public IList<V1OwnerReference> Owner { get; set; }

    }
}
