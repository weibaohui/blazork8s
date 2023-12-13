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
                Path = "/PortForward",
                Name = "PortForward",
                Key  = "PortForward",
                Icon = "setting",
            },
            new MenuDataItem
            {
                Path = "/ai/chatDeploy",
                Name = "welcome",
                Key  = "welcome",
                Icon = "setting",
            },
            new MenuDataItem
            {
                Path = "/NodeList",
                Name = "NodeList",
                Key  = "NodeList",
                Icon = "setting",
            }, new MenuDataItem
            {
                Path = "/Node",
                Name = "Nodes",
                Key  = "Nodes",
                Icon = "setting",
            },
            new MenuDataItem
            {
                Name     = "Workloads",
                Key      = "Workloads",
                Icon     = "setting",
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
            Icon = "setting",
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
                Icon = "setting",
            },
            new MenuDataItem
            {
                Path = "/Deployments",
                Name = "Deployments",
                Key  = "Deployments",
                Icon = "setting",
            },
            new MenuDataItem
            {
                Path = "/ReplicaSets",
                Name = "ReplicaSets",
                Key  = "ReplicaSets",
                Icon = "setting",
            },
            new MenuDataItem
            {
                Path = "/DaemonSet",
                Name = "DaemonSet",
                Key  = "DaemonSet",
                Icon = "setting",
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
