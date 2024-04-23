using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod
{
    public partial class MiniPodListView : PageBase
    {
        [Inject] private IPodService PodService { get; set; }

        [Inject] private DrawerService DrawerService { get; set; }

        [Parameter] public IList<V1Pod> Pods { get; set; } = new List<V1Pod>();


        [Parameter] public string ControllerByUid { get; set; }

        [Parameter] public bool HideAction { get; set; }


        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(ControllerByUid))
            {
                Pods = PodService.ListByOwnerUid(ControllerByUid);
            }

            await base.OnInitializedAsync();
        }


        async Task OnPodNameClick(V1Pod pod)
        {
            await PageDrawerHelper<V1Pod>.Instance
                .SetDrawerService(DrawerService)
                .ShowDrawerAsync<PodDetailView, V1Pod, bool>(pod);
        }
    }
}
