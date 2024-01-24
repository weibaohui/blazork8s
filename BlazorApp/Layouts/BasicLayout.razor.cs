using System.Threading.Tasks;
using AntDesign.ProLayout;
using k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Layouts;

public partial class BasicLayout : LayoutComponentBase
{
    public MenuDataItem[] MenuData;

    protected override async Task OnInitializedAsync()
    {
        MenuData = new[]
        {
            GetMenuItem("Cluster","cluster"),
            new MenuDataItem
            {
                Path = "/PortForward",
                Name = "PortForward",
                Key  = "PortForward",
                Icon = "pic-center",
                HideInMenu = KubernetesClientConfiguration.IsInCluster()
            },
            new MenuDataItem
            {
                Path = "/ai/chatDeploy",
                Name = "welcome",
                Key  = "welcome",
                Icon = "home",
            },
            GetMenuItem("Namespace","book"),
            GetMenuItem("Node","database"),
            new MenuDataItem
            {
                Name     = "Workloads",
                Key      = "Workloads",
                Icon     = "appstore",
                Children = WorkloadsMenu(),
            },
            new MenuDataItem
            {
                Name     = "Config",
                Key      = "Config",
                Icon     = "control",
                Children = ConfigMenu(),
            },
            new MenuDataItem
            {
                Name     = "Network",
                Key      = "Network",
                Icon     = "apartment",
                Children = NetworkMenu(),
            },
            new MenuDataItem
            {
                Name     = "Storage",
                Key      = "Storage",
                Icon     = "inbox",
                Children = StorageMenu(),
            },
            new MenuDataItem
            {
                Name     = "AccessControl",
                Key      = "AccessControl",
                Icon     = "verified",
                Children = AccessControlMenu(),
            },
            GetMenuItem("Crd","code"),

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
    private MenuDataItem GetMenuItem(string item,string icon)
    {
        return new MenuDataItem
        {
            Path = item,
            Name = item,
            Key  = item,
            Icon = icon,
        };
    }
    private MenuDataItem GetMenuItemWithPath(string item, string icon,string path)
    {
        return new MenuDataItem
        {
            Path = path,
            Name = item,
            Key  = item,
            Icon = icon,
        };
    }
    private MenuDataItem[] WorkloadsMenu()
    {
        return new[]
        {
            GetMenuItem("Pods","block"),
            GetMenuItem("Deployments","deployment-unit"),
            GetMenuItem("ReplicaSets","build"),
            GetMenuItem("DaemonSet","partition"),
            GetMenuItem("StatefulSet","project"),
            GetMenuItemWithPath("RC", "split-cells","ReplicationController"),
            GetMenuItem("Job","container"),
            GetMenuItem("CronJob","history"),
        };
    }

    private MenuDataItem[] ConfigMenu()
    {
        return new[]
        {
            GetMenuItem("ConfigMap","table"),
            GetMenuItem("Secret","file-protect"),
            GetMenuItem("ResourceQuota","one-to-one"),
            GetMenuItem("LimitRange","column-width"),
            GetMenuItemWithPath("HPA", "ungroup","HorizontalPodAutoscaler"),
            GetMenuItemWithPath("PDB", "appstore-add","PodDisruptionBudget"),
            GetMenuItem("PriorityClass","insert-row-left"),
            GetMenuItemWithPath("Validating","security-scan" ,"ValidatingWebhookConfiguration"),
            GetMenuItemWithPath("Mutating", "sisternode","MutatingWebhookConfiguration"),
        };
    }

    private MenuDataItem[] NetworkMenu()
    {
        return new[]
        {
            GetMenuItem("Service","partition"),
            GetMenuItem("EndpointSlice","format-painter"),
            GetMenuItem("Endpoints","node-expand"),
            GetMenuItem("NetworkPolicy","partition"),
            GetMenuItem("IngressClass","reconciliation"),
            GetMenuItem("Ingress","gateway"),
        };
    }

    private MenuDataItem[] StorageMenu()
    {
        return new[]
        {
            GetMenuItem("StorageClass","file-sync"),
            GetMenuItemWithPath("PV", "file-done","PersistentVolume"),
            GetMenuItemWithPath("PVC", "delivered-procedure","PersistentVolumeClaim"),
        };
    }

    private MenuDataItem[] AccessControlMenu()
    {
        return new[]
        {
            GetMenuItem("ServiceAccount","team"),
            GetMenuItem("ClusterRole","audit"),
            GetMenuItem("ClusterRoleBinding","api"),
            GetMenuItem("Role","idcard"),
            GetMenuItem("RoleBinding","contacts"),
        };
    }
}
