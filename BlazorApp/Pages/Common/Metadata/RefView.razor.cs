using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.ClusterRole;
using BlazorApp.Pages.ConfigMap;
using BlazorApp.Pages.CronJob;
using BlazorApp.Pages.DaemonSet;
using BlazorApp.Pages.Deployment;
using BlazorApp.Pages.Job;
using BlazorApp.Pages.Node;
using BlazorApp.Pages.PersistentVolumeClaim;
using BlazorApp.Pages.Pod;
using BlazorApp.Pages.ReplicaSet;
using BlazorApp.Pages.ReplicationController;
using BlazorApp.Pages.Role;
using BlazorApp.Pages.ServiceAccount;
using BlazorApp.Pages.StatefulSet;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.Common.Metadata;

public partial class RefView : ComponentBase
{
    [Parameter]
    public V1ObjectReference Ref { get; set; }

    [Parameter]
    public bool FullView { get; set; } = false;

    [Inject]
    private IMessageService MessageService { get; set; }

    [Inject]
    private IServiceAccountService ServiceAccountService { get; set; }

    [Inject]
    private IPersistentVolumeService PersistentVolumeService { get; set; }

    [Inject]
    private IPersistentVolumeClaimService PersistentVolumeClaimService { get; set; }

    [Inject]
    private IClusterRoleService ClusterRoleService { get; set; }

    [Inject]
    private IRoleService RoleService { get; set; }

    [Inject]
    private IReplicaSetService ReplicaSetService { get; set; }

    [Inject]
    private INodeService NodeService { get; set; }

    [Inject]
    private IJobService JobService { get; set; }

    [Inject]
    private ICronJobService CronJobService { get; set; }

    [Inject]
    private IPodService PodService { get; set; }

    [Inject]
    private IDeploymentService DeploymentService { get; set; }

    [Inject]
    private IDaemonSetService DaemonSetService { get; set; }

    [Inject]
    private IStatefulSetService StatefulSetService { get; set; }

    [Inject]
    private IReplicationControllerService ReplicationControllerService { get; set; }

    [Inject]
    private IConfigMapService ConfigMapService { get; set; }

    [Inject]
    private ILogger<ControllerBy> Logger { get; set; }

    [Inject]
    private IMessageService Message { get; set; }

    [Inject]
    private DrawerService DrawerService { get; set; }

    private async Task OnRsClick(V1ObjectReference r)
    {
        var item = ReplicaSetService.GetByName(r.NamespaceProperty,r.Name);
        await PageDrawerHelper<V1ReplicaSet>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(item);
    }

    private async Task OnNodeClick(V1ObjectReference r)
    {
        var item = NodeService.GetByName(r.NamespaceProperty,r.Name);
        await PageDrawerHelper<V1Node>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<NodeDetailView, V1Node, bool>(item);
    }

    private async Task OnDeploymentClick(V1ObjectReference r)
    {
        var item = DeploymentService.GetByName(r.NamespaceProperty,r.Name);
        await PageDrawerHelper<V1Deployment>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<DeploymentDetailView, V1Deployment, bool>(item);
    }

    private async Task OnDaemonSetClick(V1ObjectReference r)
    {
        var item = DaemonSetService.GetByName(r.NamespaceProperty,r.Name);
        await PageDrawerHelper<V1DaemonSet>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<DaemonSetDetailView, V1DaemonSet, bool>(item);
    }

    private async Task OnReplicationControllerClick(V1ObjectReference r)
    {
        var item = ReplicationControllerService.GetByName(r.NamespaceProperty,r.Name);
        await PageDrawerHelper<V1ReplicationController>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<ReplicationControllerDetailView, V1ReplicationController, bool>(item);
    }

    private Task OnXClick(V1ObjectReference r)
    {
        Message.Error($"{r.Name}点击未实现");
        return Task.CompletedTask;
    }


    private Task OnObjClick()
    {
        var name = Ref.Name;
        var kind = Ref.Kind;
        var ns   = Ref.NamespaceProperty;

        var task = kind switch
        {
            "Deployment"            => OnDeploymentClick(Ref),
            "DaemonSet"             => OnDaemonSetClick(Ref),
            "StatefulSet"           => OnStatefulSetClick(Ref),
            "ReplicationController" => OnReplicationControllerClick(Ref),
            "ReplicaSet"            => OnRsClick(Ref),
            "Node"                  => OnNodeClick(Ref),
            "Job"                   => OnJobClick(Ref),
            "CronJob"               => OnCronJobClick(Ref),
            "Pod"                   => OnPodClick(Ref),
            "Group"                 => OnGroupClick(Ref),
            "User"                  => OnUserClick(Ref),
            "ServiceAccount"        => OnServiceAccountClick(Ref),
            "ClusterRole"           => OnClusterRoleClick(Ref),
            "Role"                  => OnRoleClick(Ref),
            "ConfigMap"             => OnConfigMapClick(Ref),
            "PersistentVolumeClaim" => OnPersistentVolumeClaimClick(Ref),
            _                       => OnXClick(Ref)
        };

        return task;
    }



    private async Task OnConfigMapClick(V1ObjectReference r)
    {
        var item = ConfigMapService.GetByName(r.NamespaceProperty,r.Name);
        await PageDrawerHelper<V1ConfigMap>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<ConfigMapDetailView, V1ConfigMap, bool>(item);
    }

    private async Task OnPersistentVolumeClaimClick(V1ObjectReference r)
    {
        var item = PersistentVolumeClaimService.GetByName(r.NamespaceProperty,r.Name);
        await PageDrawerHelper<V1PersistentVolumeClaim>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<PersistentVolumeClaimDetailView, V1PersistentVolumeClaim, bool>(item);
    }

    private async Task OnUserClick(V1ObjectReference r)
    {
        await MessageService.Info(r.Name);
    }

    private async Task OnGroupClick(V1ObjectReference r)
    {
        await MessageService.Info(r.Name);
    }

    private async Task OnServiceAccountClick(V1ObjectReference r)
    {
        var item = ServiceAccountService.GetByName(r.NamespaceProperty,r.Name);
        if (item == null)
        {
            await MessageService.Error($"ServiceAccount {r.Name} Not Found");
            return;
        }

        await PageDrawerHelper<V1ServiceAccount>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<ServiceAccountDetailView, V1ServiceAccount, bool>(item);
    }

    private async Task OnClusterRoleClick(V1ObjectReference r)
    {
        var item = ClusterRoleService.GetByName(r.NamespaceProperty,r.Name);
        await PageDrawerHelper<V1ClusterRole>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<ClusterRoleDetailView, V1ClusterRole, bool>(item);
    }

    private async Task OnRoleClick(V1ObjectReference r)
    {
        var item = RoleService.GetByName(r.NamespaceProperty,r.Name);
        await PageDrawerHelper<V1Role>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<RoleDetailView, V1Role, bool>(item);
    }

    private async Task OnPodClick(V1ObjectReference r)
    {
        var item = PodService.GetByName(r.NamespaceProperty,r.Name);
        await PageDrawerHelper<V1Pod>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<PodDetailView, V1Pod, bool>(item);
    }

    private async Task OnCronJobClick(V1ObjectReference r)
    {
        var item = CronJobService.GetByName(r.NamespaceProperty,r.Name);

        await PageDrawerHelper<V1CronJob>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<CronJobDetailView, V1CronJob, bool>(item);
    }

    private async Task OnJobClick(V1ObjectReference r)
    {
        var item = JobService.GetByName(r.NamespaceProperty,r.Name);
        if (item == null)
        {
            await Message.Error($"Job {r.Name} 已被删除");
            return;
        }

        await PageDrawerHelper<V1Job>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<JobDetailView, V1Job, bool>(item);
    }

    private async Task OnStatefulSetClick(V1ObjectReference r)
    {
        var item = StatefulSetService.GetByName(r.NamespaceProperty,r.Name);
        await PageDrawerHelper<V1StatefulSet>.Instance
            .SetDrawerService(DrawerService)
            .ShowDrawerAsync<StatefulSetDetailView, V1StatefulSet, bool>(item);
    }
}
