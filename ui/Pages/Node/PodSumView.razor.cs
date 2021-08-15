using System.Collections.Generic;
using System.Threading.Tasks;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace ui.Pages.Node
{
    public partial class PodSumView : ComponentBase
    {
        [Parameter]
        public string NodeName { get; set; }

        [Parameter]
        public IList<V1Pod> Pods { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
