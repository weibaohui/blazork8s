using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Deployment;
using BlazorApp.Pages.Pod;
using BlazorApp.Pages.ReplicaSet;
using BlazorApp.Utils;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Diagrams;

public partial class KubeNodeWidget<T> : PageBase where T : IKubernetesObject<V1ObjectMeta>
{
    private string resName;

    private string typeName;
    [Parameter] public KubeNode<T> Node { get; set; }

    protected override async Task OnInitializedAsync()
    {
        typeName = Node.Item.GetType().Name;
        resName = Node.Item.Name();
        await base.OnInitializedAsync();
    }

    private async Task Show()
    {
        switch (typeName)
        {
            case "V1Deployment":
                await OnDeployClick(Node.Item as V1Deployment);
                break;
            case "V1Pod":
                await OnPodClick(Node.Item as V1Pod);
                break;
            case "V1ReplicaSet":
                await OnRsClick(Node.Item as V1ReplicaSet);
                break;
        }
    }

    private async Task OnDeployClick(V1Deployment deploy)
    {
        await PageDrawerHelper<V1Deployment>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<DeploymentDetailView, V1Deployment, bool>(deploy);
    }

    private async Task OnPodClick(V1Pod pod)
    {
        await PageDrawerHelper<V1Pod>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<PodDetailView, V1Pod, bool>(pod);
    }

    private async Task OnRsClick(V1ReplicaSet rs)
    {
        await PageDrawerHelper<V1ReplicaSet>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(rs);
    }
}
