using System;
using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace ui.Pages.Container
{
    public partial class ContainerListView : ComponentBase
    {
         
        public IList<V1Container> Containers { get; set; }
        public IList<V1ContainerStatus> ContainerStatuses { get; set; }

        [Parameter]
        public V1Pod Pod { get; set; }


        protected override void OnInitialized()
        {
            Containers = Pod.Spec.Containers;
            ContainerStatuses = Pod.Status.ContainerStatuses;
        }
    }
}
