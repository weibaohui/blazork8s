using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using  BlazorApp.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace  BlazorApp.Pages.Pod
{
    public partial class PodSumView : ComponentBase
    {
        [Parameter]
        public string NodeName { get; set; }

        [Parameter]
        public IList<V1Pod> Pods { get; set; }


        [Inject]
        private IPodService PodService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async Task OnPodClick(V1Pod pod)
        {
            await PodService.ShowPodDrawer(pod);
        }
    }
}
