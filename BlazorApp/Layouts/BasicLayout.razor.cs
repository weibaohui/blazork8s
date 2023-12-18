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
            },
            new MenuDataItem
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
            new MenuDataItem
            {
                Name     = "Config",
                Key      = "Config",
                Icon     = "setting",
                Children = ConfigMenu(),
            },
            new MenuDataItem
            {
                Name     = "Network",
                Key      = "Network",
                Icon     = "setting",
                Children = NetworkMenu(),
            },
            new MenuDataItem
            {
                Name     = "Storage",
                Key      = "Storage",
                Icon     = "setting",
                Children = StorageMenu(),
            },
            new MenuDataItem
            {
                Name     = "AccessControl",
                Key      = "AccessControl",
                Icon     = "setting",
                Children = AccessControlMenu(),
            },
        };
        await base.OnInitializedAsync();
    }

    private MenuDataItem GetMenuItem(string item)
    {
        return new MenuDataItem
        {
            Path = item,
            Name = item,
            Key  = item,
            Icon = "setting",
        };
    }

    private MenuDataItem GetMenuItem(string name, string path)
    {
        return new MenuDataItem
        {
            Path = path,
            Name = name,
            Key  = path,
            Icon = "setting",
        };
    }

    private MenuDataItem GetMenuItem(string name, string path, string icon)
    {
        return new MenuDataItem
        {
            Path = path,
            Name = name,
            Key  = path,
            Icon = icon,
        };
    }

    private MenuDataItem[] WorkloadsMenu()
    {
        return new[]
        {
            GetMenuItem("Pods"),
            GetMenuItem("Deployments"),
            GetMenuItem("ReplicaSets"),
            GetMenuItem("DaemonSet"),
            GetMenuItem("RC", "ReplicationController"),
            GetMenuItem("Job"),
            GetMenuItem("CronJob"),
        };
    }

    private MenuDataItem[] ConfigMenu()
    {
        return new[]
        {
            GetMenuItem("ConfigMap"),
            GetMenuItem("Secret"),
            GetMenuItem("ResourceQuota"),
            GetMenuItem("LimitRange"),
            GetMenuItem("HPA", "HorizontalPodAutoscaler"),
            GetMenuItem("PDB", "PodDisruptionBudget"),
            GetMenuItem("PriorityClass"),
            GetMenuItem("Validating", "ValidatingWebhookConfiguration"),
            GetMenuItem("Mutating", "MutatingWebhookConfiguration"),
        };
    }

    private MenuDataItem[] NetworkMenu()
    {
        return new[]
        {
            GetMenuItem("Service"),
            GetMenuItem("EndpointSlice"),
            GetMenuItem("Endpoints"),
            GetMenuItem("NetworkPolicy"),
            GetMenuItem("IngressClass"),
            GetMenuItem("Ingress"),
        };
    }

    private MenuDataItem[] StorageMenu()
    {
        return new[]
        {
            GetMenuItem("StorageClass"),
            GetMenuItem("PV", "PersistentVolume"),
            GetMenuItem("PVC", "PersistentVolumeClaim"),
        };
    }

    private MenuDataItem[] AccessControlMenu()
    {
        return new[]
        {
            GetMenuItem("ServiceAccount"),
            GetMenuItem("ClusterRole"),
            GetMenuItem("ClusterRoleBinding"),
            GetMenuItem("Role"),
            GetMenuItem("RoleBinding"),
        };
    }
}
