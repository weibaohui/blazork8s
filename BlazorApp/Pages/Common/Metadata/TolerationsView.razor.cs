using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Common.Metadata
{
    public partial class TolerationsView : PageBase
    {
        [Parameter]
        public IList<V1Toleration> Tolerations { get; set; }
    }
}
