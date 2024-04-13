using System;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ResourceCrudService(
    IKubeService KubeService
) : IResourceCrudService
{
    public async Task<object> DeleteItem(string kind, string ns, string name)
    {
        return kind switch
        {
            "Deployment" => OnDeploymentDelete(ns, name),
            "DaemonSet" => OnDaemonSetDelete(ns, name),
            "StatefulSet" => OnStatefulSetDelete(ns, name),
            "ReplicationController" => OnReplicationControllerDelete(ns, name),
            "ReplicaSet" => OnRsDelete(ns, name),
            "Node" => OnNodeDelete(ns, name),
            "Job" => OnJobDelete(ns, name),
            "CronJob" => OnCronJobDelete(ns, name),
            "Pod" => OnPodDelete(ns, name),
            "Service" => OnServiceDelete(ns, name),
            "Endpoints" => OnEndpointsDelete(ns, name),
            "ServiceAccount" => OnServiceAccountDelete(ns, name),
            "ClusterRole" => OnClusterRoleDelete(ns, name),
            "Role" => OnRoleDelete(ns, name),
            "HorizontalPodAutoscaler" => OnHorizontalPodAutoscalerDelete(ns, name),
            "ConfigMap" => OnConfigMapDelete(ns, name),
            "Secret" => OnSecretDelete(ns, name),
            "Ingress" => OnIngressDelete(ns, name),
            "Namespace" => OnNamespaceDelete(ns, name),
            "PersistentVolumeClaim" => OnPersistentVolumeClaimDelete(ns, name),
            "ResourceQuota" => OnResourceQuotaDelete(ns, name),
            "LimitRange" => OnLimitRangeDelete(ns, name),
            "Lease" => OnLeaseDelete(ns, name),
            "PodDisruptionBudget" => OnPodDisruptionBudgetDelete(ns, name),
            "PriorityClass" => OnPriorityClassDelete(ns, name),
            "ValidatingWebhookConfiguration" => OnValidatingWebhookConfigurationDelete(ns, name),
            "MutatingWebhookConfiguration" => OnMutatingWebhookConfigurationDelete(ns, name),
            "EndpointSlice" => OnEndpointSliceDelete(ns, name),
            "NetworkPolicy" => OnNetworkPolicyDelete(ns, name),
            "IngressClass" => OnIngressClassDelete(ns, name),
            "StorageClass" => OnStorageClassDelete(ns, name),
            "PersistentVolume" => OnPersistentVolumeDelete(ns, name),
            "ClusterRoleBinding" => OnClusterRoleBindingDelete(ns, name),
            "RoleBinding" => OnRoleBindingDelete(ns, name),
            "CustomResourceDefinition" => OnCustomResourceDefinitionDelete(ns, name),
            _ => OnXDelete(kind, ns, name)
        };
    }

    private async Task<V1PersistentVolumeClaim> OnPersistentVolumeClaimDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedPersistentVolumeClaimAsync(name, ns);
    }

    private async Task<V1Status> OnNamespaceDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespaceAsync(name);
    }

    private async Task<V1Status> OnIngressDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedIngressAsync(name, ns);
    }

    private async Task<V1Status> OnConfigMapDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedConfigMapAsync(name, ns);
    }

    private async Task<V1Status> OnSecretDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedSecretAsync(name, ns);
    }

    private async Task<V1Status> OnHorizontalPodAutoscalerDelete(string ns, string name)
    {
        return await KubeService.Client().AutoscalingV2.DeleteNamespacedHorizontalPodAutoscalerAsync(name, ns);
    }

    private async Task<V1Status> OnRoleDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedRoleAsync(name, ns);
    }

    private async Task<V1Status> OnClusterRoleDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteClusterRoleAsync(name);
    }

    private async Task<V1ServiceAccount> OnServiceAccountDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedServiceAccountAsync(name, ns);
    }

    private async Task<V1Status> OnEndpointsDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedEndpointsAsync(name, ns);
    }

    private async Task<V1Service> OnServiceDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedServiceAsync(name, ns);
    }

    private async Task<V1Pod> OnPodDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedPodAsync(name, ns);
    }

    private async Task<V1Status> OnCronJobDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedCronJobAsync(name, ns);
    }

    private async Task<V1Status> OnJobDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedJobAsync(name, ns);
    }

    private async Task<V1Status> OnNodeDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNodeAsync(name);
    }

    private async Task<V1Status> OnRsDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedReplicaSetAsync(name, ns);
    }

    private async Task<V1Status> OnReplicationControllerDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedReplicationControllerAsync(name, ns);
    }

    private async Task<V1Status> OnStatefulSetDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedStatefulSetAsync(name, ns);
    }

    private async Task<V1Status> OnDaemonSetDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedDaemonSetAsync(name, ns);
    }

    private async Task<V1Status> OnXDelete(string kind, string ns, string name)
    {
        Console.WriteLine($"{kind}  {ns} {name} delete not implanted yet");
        return null;
    }

    private async Task<V1Status> OnDeploymentDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedDeploymentAsync(name, ns);
    }


    private async Task<V1Status> OnResourceQuotaDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedDeploymentAsync(name, ns);
    }

    private async Task<V1Status> OnLimitRangeDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedLimitRangeAsync(name, ns);
    }

    private async Task<V1Status> OnLeaseDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedLeaseAsync(name, ns);
    }

    private async Task<V1Status> OnPodDisruptionBudgetDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedPodDisruptionBudgetAsync(name, ns);
    }

    private async Task<V1Status> OnPriorityClassDelete(string ns, string name)
    {
        return await KubeService.Client().DeletePriorityClassAsync(name);
    }

    private async Task<V1Status> OnValidatingWebhookConfigurationDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteValidatingWebhookConfigurationAsync(name);
    }

    private async Task<V1Status> OnMutatingWebhookConfigurationDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteMutatingWebhookConfigurationAsync(name);
    }

    private async Task<V1Status> OnEndpointSliceDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedEndpointSliceAsync(name, ns);
    }

    private async Task<V1Status> OnNetworkPolicyDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedNetworkPolicyAsync(name, ns);
    }

    private async Task<V1Status> OnIngressClassDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteIngressClassAsync(name);
    }

    private async Task<V1StorageClass> OnStorageClassDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteStorageClassAsync(name);
    }

    private async Task<V1PersistentVolume> OnPersistentVolumeDelete(string ns, string name)
    {
        return await KubeService.Client().DeletePersistentVolumeAsync(name);
    }

    private async Task<V1Status> OnClusterRoleBindingDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteClusterRoleBindingAsync(name);
    }

    private async Task<V1Status> OnRoleBindingDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteNamespacedRoleBindingAsync(name, ns);
    }

    private async Task<V1Status> OnCustomResourceDefinitionDelete(string ns, string name)
    {
        return await KubeService.Client().DeleteCustomResourceDefinitionAsync(name);
    }
}
