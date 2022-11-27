using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service;
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
        private IPageDrawerService PageDrawerService { get; set; }

        [Inject]
        private IPodService PodService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async Task OnPodClick(V1Pod pod)
        {
            var options = PageDrawerService.DefaultOptions($"{pod.Kind}:{pod.Name()}");
            await PageDrawerService.ShowDrawerAsync<PodDetailView, V1Pod, bool>(options, pod);
        }
    }
}
