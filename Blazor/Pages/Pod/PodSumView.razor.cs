using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Pod
{
    public partial class PodSumView : ComponentBase
    {
        [Parameter]
        public string NodeName { get; set; }

        [Parameter]
        public IList<V1Pod> Pods { get; set; }


        [Inject]
        private DrawerService DrawerService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        async Task OnPodClick(V1Pod pod)
        {
            var options = new DrawerOptions
            {
                Title = "POD:" + pod.Name(),
                Width = 800
            };
            var drawerRef =
                await DrawerService.CreateAsync<PodDetailView, V1Pod, bool>(options,
                    pod);
        }
    }
}