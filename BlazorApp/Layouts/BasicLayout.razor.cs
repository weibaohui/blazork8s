using System.Threading.Tasks;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Layouts;

public partial class BasicLayout : LayoutComponentBase
{
    public MenuDataItem[] MenuData;

    protected override async Task OnInitializedAsync()
    {
        MenuData = new[]
        {
            new MenuDataItem
            {
                Path = "/ai/chatDeploy",
                Name = "welcome",
                Key  = "welcome",
                Icon = "smile",
            },
            new MenuDataItem
            {
                Path = "/NodeList",
                Name = "NodeList",
                Key  = "NodeList",
                Icon = "smile",
            }, new MenuDataItem
            {
                Path = "/Node",
                Name = "Nodes",
                Key  = "Nodes",
                Icon = "smile",
            },
            new MenuDataItem
            {
                Name     = "Workloads",
                Key      = "Workloads",
                Icon     = "smile",
                Children = WorkloadsMenu(),
            },
        };
        await base.OnInitializedAsync();
    }

    private  MenuDataItem  GetMenuItem(string item)
    {
        return new MenuDataItem
        {
            Path = item,
            Name = item,
            Key  = item,
            Icon = "smile",
        };
    }

    private MenuDataItem[] WorkloadsMenu()
    {
        return new[]
        {
            new MenuDataItem
            {
                Path = "/Pods",
                Name = "Pods",
                Key  = "Pods",
                Icon = "smile",
            },
            new MenuDataItem
            {
                Path = "/Deployments",
                Name = "Deployments",
                Key  = "Deployments",
                Icon = "smile",
            },
            new MenuDataItem
            {
                Path = "/ReplicaSets",
                Name = "ReplicaSets",
                Key  = "ReplicaSets",
                Icon = "smile",
            },
            new MenuDataItem
            {
                Path = "/DaemonSet",
                Name = "DaemonSet",
                Key  = "DaemonSet",
                Icon = "smile",
            },
            GetMenuItem("Job"),
            GetMenuItem("Service"),
            GetMenuItem("ServiceAccount"),
            GetMenuItem("ClusterRole"),
            GetMenuItem("ClusterRoleBinding"),
            GetMenuItem("Role"),
            GetMenuItem("RoleBinding"),
            GetMenuItem("Ingress"),
            GetMenuItem("PersistentVolume"),
            GetMenuItem("PersistentVolumeClaim"),
            GetMenuItem("StorageClass"),
            GetMenuItem("NetworkPolicy"),
            GetMenuItem("IngressClass"),
            GetMenuItem("EndpointSlice"),
            GetMenuItem("Endpoints"),
            GetMenuItem("Secret"),
            GetMenuItem("PriorityClass"),
            GetMenuItem("PodDisruptionBudget"),
            GetMenuItem("ValidatingWebhookConfiguration"),
            GetMenuItem("MutatingWebhookConfiguration"),
            GetMenuItem("LimitRange"),
            GetMenuItem("HorizontalPodAutoscaler"),
            GetMenuItem("ResourceQuota"),
            GetMenuItem("ConfigMap"),
            GetMenuItem("CronJob"),
            GetMenuItem("StatefulSet"),
            GetMenuItem("ReplicationController"),
        };
    }
}
