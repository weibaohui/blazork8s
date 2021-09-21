using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Container
{
    public partial class ContainerView : ComponentBase
    {
        [Parameter]
        public IList<V1Container> Containers { get; set; }
        [Parameter]
        public IList<V1ContainerStatus> ContainerStatuses { get; set; }

        [Parameter]
        public string Type { get; set; }

    }
}
