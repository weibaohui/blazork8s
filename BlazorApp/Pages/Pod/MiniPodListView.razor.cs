using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod;

public partial class MiniPodListView : PageBase
{
    [Inject] private IPodService PodService { get; set; }

    [Parameter] public IList<V1Pod> Pods { get; set; } = new List<V1Pod>();


    [Parameter] public string ControllerByUid { get; set; }


    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(ControllerByUid)) Pods = PodService.ListByOwnerUid(ControllerByUid);

        await base.OnInitializedAsync();
    }


    private async Task OnPodNameClick(V1Pod pod)
    {
        await PageDrawerHelper<V1Pod>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<PodDetailView, V1Pod, bool>(pod);
    }
}
