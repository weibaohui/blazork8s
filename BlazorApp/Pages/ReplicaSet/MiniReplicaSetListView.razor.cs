using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Extension.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ReplicaSet;

public partial class MiniReplicaSetListView : PageBase
{
    [Inject] private IReplicaSetService ReplicaSetService { get; set; }

    [Inject] private IPodService PodService { get; set; }
    [Parameter] public string CurrentRevision { get; set; }

    private IList<V1Pod> PodList { get; set; }
    private IList<V1ReplicaSet> Items { get; set; }

    [Parameter] public string ControllerByUid { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(ControllerByUid))
        {
            Items = ReplicaSetService.ListByOwnerUid(ControllerByUid);
        }

        PodList = PodService.List();
        await base.OnInitializedAsync();
    }


    private async Task OnRsClick(V1ReplicaSet rs)
    {
        await PageDrawerHelper<V1ReplicaSet>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(rs);
    }

    private int CountPodsByOwner(string uid)
    {
        return PodList.CountPodsByOwner(uid);
    }

    private async Task OnRolloutClick(V1ReplicaSet rs)
    {
        var name = rs.Name();
        var revision = rs.Metadata?.Annotations?["deployment.kubernetes.io/revision"];
        var command = $"  rollout undo deployment/nginx-web --to-revision={revision}";
        var options = PageDrawerService.DefaultOptions($"rollout undo", width: 1000);
        await PageDrawerService.ShowDrawerAsync<KubectlCommand, string, bool>(options, command);
    }
}
