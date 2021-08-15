using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace ui.Pages.Node
{
    public partial class PodListView : ComponentBase
    {
        [Parameter]
        public IList<V1Pod> Pods { get; set; }
    }
}
