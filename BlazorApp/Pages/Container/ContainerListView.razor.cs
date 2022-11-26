using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace  BlazorApp.Pages.Container
{
    public partial class ContainerListView : ComponentBase
    {
        [Parameter]
        public V1Pod Pod { get; set; }
    }
}
