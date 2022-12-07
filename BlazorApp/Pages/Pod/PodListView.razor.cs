using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod
{
    public partial class PodListView : ComponentBase
    {
        [Inject]
        private IPodService PodService { get; set; }

        [Inject]
        private IPageDrawerService PageDrawerService { get; set; }

        [Parameter]
        public IList<V1Pod> Pods { get; set; } = new List<V1Pod>();


        [Parameter]
        public string ControllerByUid { get; set; }


        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(ControllerByUid))
            {
                Pods = await PodService.ListByOwnerUid(ControllerByUid);
            }

            await base.OnInitializedAsync();
        }


        async Task OnPodNameClick(V1Pod pod)
        {
            var options = PageDrawerService.DefaultOptions($"{pod.Kind ?? "Pod"}:{pod.Name()}");
            await PageDrawerService.ShowDrawerAsync<PodDetailView, V1Pod, bool>(options, pod);
        }
    }
}
