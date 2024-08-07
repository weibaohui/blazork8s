using System;
using System.Threading.Tasks;
using BlazorApp.Chat;
using BlazorApp.Utils;
using Entity.Crd.Gateway;
using k8s.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.k8s;

public class ListWatchService
{
    private readonly IHubContext<ChatHub> _ctx;
    private readonly IKubeService _kubeService;
    private readonly ILogger<ListWatchService> _logger = LoggingHelper<ListWatchService>.Logger();

    public ListWatchService(IKubeService kubeService, IHubContext<ChatHub> ctx)
    {
        Console.WriteLine("ListWatchService 初始化" + DateTime.Now);
        _kubeService = kubeService;
        _ctx = ctx;
    }

    public void Dispose()
    {
    }

    public Task StartAsync()
    {
        Console.WriteLine("ListWatchService 启动" + DateTime.Now);
#pragma warning disable CS4014
        WatchPod();
        WatchDeployment();
        WatchReplicaSet();
        WatchNode();
        WatchEvent();
        WatchNamespace();
        WatchDaemonSet();
        WatchReplicationController();
        WatchStatefulSet();
        WatchJob();
        WatchCronJob();
        WatchConfigMap();
        WatchSecret();
        WatchResourceQuota();
        WatchLimitRange();
        WatchV2HorizontalPodAutoscaler();
        WatchMutatingWebhookConfiguration();
        WatchValidatingWebhookConfiguration();
        WatchPriorityClass();
        WatchPodDisruptionBudget();
        WatchEndpoints();
        WatchEndpointSlice();
        WatchService();
        WatchIngress();
        WatchIngressClass();
        WatchNetworkPolicy();
        WatchStorageClass();
        WatchPersistentVolume();
        WatchPersistentVolumeClaim();
        WatchRole();
        WatchRoleBinding();
        WatchClusterRole();
        WatchClusterRoleBinding();
        WatchServiceAccount();
        WatchCrd();
        WatchLease();
        WatchGatewayClass();
        WatchGateway();
        WatchHttpRoute();
        WatchGrpcRoute();
        WatchTcpRoute();
        WatchUdpRoute();
        WatchReferenceGrant();
        WatchBackendTLSPolicy();
#pragma warning restore CS4014
        return Task.CompletedTask;
    }

    private async Task WatchCrd()
    {
        var listResp = _kubeService.Client().ApiextensionsV1
            .ListCustomResourceDefinitionWithHttpMessagesAsync(watch: true);
        await new Watcher<V1CustomResourceDefinition, V1CustomResourceDefinitionList>(_ctx).Watch(listResp);
    }

    private async Task WatchPod()
    {
        var listResp = _kubeService.Client().CoreV1
            .ListPodForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1Pod, V1PodList>(_ctx).Watch(listResp);
    }

    private async Task WatchLease()
    {
        var listResp = _kubeService.Client().CoordinationV1
            .ListLeaseForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1Lease, V1LeaseList>(_ctx).Watch(listResp);
    }

    private async Task WatchServiceAccount()
    {
        var listResp = _kubeService.Client().CoreV1
            .ListServiceAccountForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1ServiceAccount, V1ServiceAccountList>(_ctx).Watch(listResp);
    }

    private async Task WatchClusterRole()
    {
        var listResp = _kubeService.Client().RbacAuthorizationV1
            .ListClusterRoleWithHttpMessagesAsync(watch: true);

        await new Watcher<V1ClusterRole, V1ClusterRoleList>(_ctx).Watch(listResp);
    }

    private async Task WatchRole()
    {
        var listResp = _kubeService.Client().RbacAuthorizationV1
            .ListRoleForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1Role, V1RoleList>(_ctx).Watch(listResp);
    }

    private async Task WatchClusterRoleBinding()
    {
        var listResp = _kubeService.Client().RbacAuthorizationV1
            .ListClusterRoleBindingWithHttpMessagesAsync(watch: true);

        await new Watcher<V1ClusterRoleBinding, V1ClusterRoleBindingList>(_ctx).Watch(listResp);
    }

    private async Task WatchRoleBinding()
    {
        var listResp = _kubeService.Client().RbacAuthorizationV1
            .ListRoleBindingForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1RoleBinding, V1RoleBindingList>(_ctx).Watch(listResp);
    }

    private async Task WatchPersistentVolume()
    {
        var listResp = _kubeService.Client().CoreV1
            .ListPersistentVolumeWithHttpMessagesAsync(watch: true);

        await new Watcher<V1PersistentVolume, V1PersistentVolumeList>(_ctx).Watch(listResp);
    }

    private async Task WatchPersistentVolumeClaim()
    {
        var listResp = _kubeService.Client().CoreV1
            .ListPersistentVolumeClaimForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1PersistentVolumeClaim, V1PersistentVolumeClaimList>(_ctx).Watch(listResp);
    }

    private async Task WatchStorageClass()
    {
        var listResp = _kubeService.Client().StorageV1
            .ListStorageClassWithHttpMessagesAsync(watch: true);

        await new Watcher<V1StorageClass, V1StorageClassList>(_ctx).Watch(listResp);
    }

    private async Task WatchIngress()
    {
        var listResp = _kubeService.Client().NetworkingV1
            .ListIngressForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1Ingress, V1IngressList>(_ctx).Watch(listResp);
    }

    private async Task WatchNetworkPolicy()
    {
        var listResp = _kubeService.Client().NetworkingV1
            .ListNetworkPolicyForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1NetworkPolicy, V1NetworkPolicyList>(_ctx).Watch(listResp);
    }

    private async Task WatchIngressClass()
    {
        var listResp = _kubeService.Client().NetworkingV1
            .ListIngressClassWithHttpMessagesAsync(watch: true);

        await new Watcher<V1IngressClass, V1IngressClassList>(_ctx).Watch(listResp);
    }

    private async Task WatchService()
    {
        var listResp = _kubeService.Client().CoreV1
            .ListServiceForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1Service, V1ServiceList>(_ctx).Watch(listResp);
    }

    private async Task WatchEndpointSlice()
    {
        var listResp = _kubeService.Client().DiscoveryV1
            .ListEndpointSliceForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1EndpointSlice, V1EndpointSliceList>(_ctx).Watch(listResp);
    }

    private async Task WatchEndpoints()
    {
        var listResp = _kubeService.Client().CoreV1
            .ListEndpointsForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1Endpoints, V1EndpointsList>(_ctx).Watch(listResp);
    }

    private async Task WatchPodDisruptionBudget()
    {
        var listResp = _kubeService.Client().PolicyV1
            .ListPodDisruptionBudgetForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1PodDisruptionBudget, V1PodDisruptionBudgetList>(_ctx).Watch(listResp);
    }

    private async Task WatchPriorityClass()
    {
        var listResp = _kubeService.Client().SchedulingV1
            .ListPriorityClassWithHttpMessagesAsync(watch: true);

        await new Watcher<V1PriorityClass, V1PriorityClassList>(_ctx).Watch(listResp);
    }

    private async Task WatchV1HorizontalPodAutoscaler()
    {
        var listResp = _kubeService.Client().AutoscalingV1
            .ListHorizontalPodAutoscalerForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1HorizontalPodAutoscaler, V1HorizontalPodAutoscalerList>(_ctx).Watch(listResp);
    }

    private async Task WatchMutatingWebhookConfiguration()
    {
        var listResp = _kubeService.Client().AdmissionregistrationV1
            .ListMutatingWebhookConfigurationWithHttpMessagesAsync(watch: true);

        await new Watcher<V1MutatingWebhookConfiguration, V1MutatingWebhookConfigurationList>(_ctx).Watch(listResp);
    }

    private async Task WatchValidatingWebhookConfiguration()
    {
        var listResp = _kubeService.Client().AdmissionregistrationV1
            .ListValidatingWebhookConfigurationWithHttpMessagesAsync(watch: true);

        await new Watcher<V1ValidatingWebhookConfiguration, V1ValidatingWebhookConfigurationList>(_ctx).Watch(listResp);
    }

    private async Task WatchV2HorizontalPodAutoscaler()
    {
        var listResp = _kubeService.Client().AutoscalingV2
            .ListHorizontalPodAutoscalerForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V2HorizontalPodAutoscaler, V2HorizontalPodAutoscalerList>(_ctx).Watch(listResp);
    }

    private async Task WatchLimitRange()
    {
        var listResp = _kubeService.Client().CoreV1
            .ListLimitRangeForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1LimitRange, V1LimitRangeList>(_ctx).Watch(listResp);
    }

    private async Task WatchResourceQuota()
    {
        var listResp = _kubeService.Client().CoreV1
            .ListResourceQuotaForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1ResourceQuota, V1ResourceQuotaList>(_ctx).Watch(listResp);
    }

    private async Task WatchSecret()
    {
        var listResp = _kubeService.Client().CoreV1
            .ListSecretForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1Secret, V1SecretList>(_ctx).Watch(listResp);
    }

    private async Task WatchConfigMap()
    {
        var listResp = _kubeService.Client().CoreV1
            .ListConfigMapForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1ConfigMap, V1ConfigMapList>(_ctx).Watch(listResp);
    }

    private async Task WatchJob()
    {
        var listResp = _kubeService.Client().BatchV1
            .ListJobForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1Job, V1JobList>(_ctx).Watch(listResp);
    }

    private async Task WatchCronJob()
    {
        var listResp = _kubeService.Client().BatchV1
            .ListCronJobForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1CronJob, V1CronJobList>(_ctx).Watch(listResp);
    }

    private async Task WatchReplicationController()
    {
        var listResp = _kubeService.Client().CoreV1
            .ListReplicationControllerForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1ReplicationController, V1ReplicationControllerList>(_ctx).Watch(listResp);
    }

    private async Task WatchStatefulSet()
    {
        var listResp = _kubeService.Client().AppsV1
            .ListStatefulSetForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1StatefulSet, V1StatefulSetList>(_ctx).Watch(listResp);
    }

    private async Task WatchDaemonSet()
    {
        var listResp = _kubeService.Client().AppsV1
            .ListDaemonSetForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1DaemonSet, V1DaemonSetList>(_ctx).Watch(listResp);
    }

    private async Task WatchEvent()
    {
        var listResp = _kubeService.Client().CoreV1
            .ListEventForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<Corev1Event, Corev1EventList>(_ctx).Watch(listResp);
    }

    private async Task WatchDeployment()
    {
        var listResp = _kubeService.Client().AppsV1.ListDeploymentForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1Deployment, V1DeploymentList>(_ctx).Watch(listResp);
    }

    private async Task WatchReplicaSet()
    {
        var listResp = _kubeService.Client().AppsV1.ListReplicaSetForAllNamespacesWithHttpMessagesAsync(watch: true);

        await new Watcher<V1ReplicaSet, V1ReplicaSetList>(_ctx).Watch(listResp);
    }

    private async Task WatchNode()
    {
        var listResp = _kubeService.Client().CoreV1.ListNodeWithHttpMessagesAsync(watch: true);

        await new Watcher<V1Node, V1NodeList>(_ctx).Watch(listResp);
    }

    private async Task WatchNamespace()
    {
        var listResp = _kubeService.Client().CoreV1.ListNamespaceWithHttpMessagesAsync(watch: true);

        await new Watcher<V1Namespace, V1NamespaceList>(_ctx).Watch(listResp);
    }


    private async Task WatchGatewayClass()
    {
        var listResp = _kubeService.Client().CustomObjects
            .ListClusterCustomObjectWithHttpMessagesAsync<V1GatewayClassList>
                ("gateway.networking.k8s.io", "v1", "gatewayclasses", watch: true);
        await new Watcher<V1GatewayClass, V1GatewayClassList>(_ctx).Watch(listResp);
    }

    private async Task WatchGateway()
    {
        var listResp = _kubeService.Client().CustomObjects
            .ListClusterCustomObjectWithHttpMessagesAsync<V1GatewayList>
                ("gateway.networking.k8s.io", "v1", "gateways", watch: true);
        await new Watcher<V1Gateway, V1GatewayList>(_ctx).Watch(listResp);
    }

    private async Task WatchHttpRoute()
    {
        var listResp = _kubeService.Client().CustomObjects
            .ListClusterCustomObjectWithHttpMessagesAsync<V1HTTPRouteList>
                ("gateway.networking.k8s.io", "v1", "httproutes", watch: true);
        await new Watcher<V1HTTPRoute, V1HTTPRouteList>(_ctx).Watch(listResp);
    }

    private async Task WatchGrpcRoute()
    {
        var listResp = _kubeService.Client().CustomObjects
            .ListClusterCustomObjectWithHttpMessagesAsync<V1GRPCRouteList>
                ("gateway.networking.k8s.io", "v1", "grpcroutes", watch: true);
        await new Watcher<V1GRPCRoute, V1GRPCRouteList>(_ctx).Watch(listResp);
    }

    private async Task WatchTcpRoute()
    {
        var listResp = _kubeService.Client().CustomObjects
            .ListClusterCustomObjectWithHttpMessagesAsync<V1Alpha2TCPRouteList>
                ("gateway.networking.k8s.io", "v1alpha2", "tcproutes", watch: true);
        await new Watcher<V1Alpha2TCPRoute, V1Alpha2TCPRouteList>(_ctx).Watch(listResp);
    }

    private async Task WatchUdpRoute()
    {
        var listResp = _kubeService.Client().CustomObjects
            .ListClusterCustomObjectWithHttpMessagesAsync<V1Alpha2UDPRouteList>
                ("gateway.networking.k8s.io", "v1alpha2", "udproutes", watch: true);
        await new Watcher<V1Alpha2UDPRoute, V1Alpha2UDPRouteList>(_ctx).Watch(listResp);
    }

    private async Task WatchReferenceGrant()
    {
        var listResp = _kubeService.Client().CustomObjects
            .ListClusterCustomObjectWithHttpMessagesAsync<V1Alpha2ReferenceGrantList>
                ("gateway.networking.k8s.io", "v1alpha2", "referencegrants", watch: true);
        await new Watcher<V1Alpha2ReferenceGrant, V1Alpha2ReferenceGrantList>(_ctx).Watch(listResp);
    }

    private async Task WatchBackendTLSPolicy()
    {
        var listResp = _kubeService.Client().CustomObjects
            .ListClusterCustomObjectWithHttpMessagesAsync<V1Alpha3BackendTLSPolicyList>
                ("gateway.networking.k8s.io", "v1alpha3", "backendtlspolicies", watch: true);
        await new Watcher<V1Alpha3BackendTLSPolicy, V1Alpha3BackendTLSPolicyList>(_ctx).Watch(listResp);
    }
}