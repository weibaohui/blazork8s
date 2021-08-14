using System.Threading.Tasks;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using ui.Service;

namespace ui.Pages.Node
{
    public partial class PodSumView : ComponentBase
    {
        private V1PodList _pods;

        [Parameter]
        public string NodeName { get; set; }

        [Inject]
        private IPodService PodService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _pods = await PodService.List();
            await base.OnInitializedAsync();
        }
    }
}
