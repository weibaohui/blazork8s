using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace ui.Pages.Node
{
    public partial class NodeResourceQuantityView : ComponentBase
    {
        [Parameter]
        public string BlockTitle { get; set; }

        [Parameter]
        public IDictionary<string, ResourceQuantity> Dict { get; set; }
    }
}
