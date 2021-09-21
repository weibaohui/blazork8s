using System.Collections.Generic;
using AntDesign;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Workload
{
    public partial class TolerationsView :ComponentBase
    {
        [Parameter]
        public IList<V1Toleration> Tolerations { get; set; }

    }
}
