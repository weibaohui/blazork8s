using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod
{
    public partial class PodSumView : ComponentBase
    {
        [Parameter]
        public string NodeName { get; set; }

        [Parameter]
        public IList<V1Pod> Pods { get; set; }

        [Inject]
        private DrawerService DrawerService { get; set; }

        [Inject]
        private IPodService PodService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async Task OnPodClick(V1Pod pod)
        {
            await PageDrawerHelper<V1Pod>.Instance
                .SetDrawerService(DrawerService)
                .ShowDrawerAsync<PodDetailView, V1Pod, bool>(pod);
        }
    }
}
