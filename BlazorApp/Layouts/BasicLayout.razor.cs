using System.Threading.Tasks;
using AntDesign.ProLayout;
using BlazorApp.Service.AI;
using k8s;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BlazorApp.Layouts;

public partial class BasicLayout : LayoutComponentBase
{
    public MenuDataItem[] MenuData;

    [Inject]
    public IStringLocalizer L { get; set; }

    [Inject]
    private IAiService Ai { get; set; }

    protected override async Task OnInitializedAsync()
    {
        MenuData =
        [
            new MenuDataItem
            {
                Name     = L["Cluster"],
                Key      = "Cluster",
                Icon     = "cluster",
                Children = ClusterMenu(),
            },
            new MenuDataItem
            {
                Path       = "/PortForward",
                Name       = L["PortForward"],
                Key        = "PortForward",
                Icon       = "pic-center",
                HideInMenu = KubernetesClientConfiguration.IsInCluster()
            },

            GetMenuItemWithPath(L["Namespace"], "book", "/Namespace"),
            GetMenuItemWithPath(L["Node"], "database", "/Node"),
            new MenuDataItem
            {
                Name     = L["Workloads"],
                Key      = "Workloads",
                Icon     = "appstore",
                Children = WorkloadsMenu(),
            },
            new MenuDataItem
            {
                Name     = L["Config"],
                Key      = "Config",
                Icon     = "control",
                Children = ConfigMenu(),
            },
            new MenuDataItem
            {
                Name     = L["Network"],
                Key      = "Network",
                Icon     = "apartment",
                Children = NetworkMenu(),
            },
            new MenuDataItem
            {
                Name     = L["Storage"],
                Key      = "Storage",
                Icon     = "inbox",
                Children = StorageMenu(),
            },
            new MenuDataItem
            {
                Name     = L["AccessControl"],
                Key      = "AccessControl",
                Icon     = "verified",
                Children = AccessControlMenu(),
            },
            GetMenuItem(L["CRD"], "code"),
            new MenuDataItem
            {
                Path       = "/ChatDeploy",
                Name       = L["ChatDeploy"],
                Key        = "ChatDeploy",
                Icon       = "robot",
                HideInMenu = !Ai.Enabled(),
            },
            GetMenuItemWithPath(L["Example"], "unordered-list", "/Example"),
            GetMenuItemWithPath(L["OpenSource"], "github", "/OpenSource")
        ];
        await base.OnInitializedAsync();
    }

    private MenuDataItem[] ClusterMenu()
    {
        return new[]
        {
            GetMenuItemWithPath("Overview", "cluster", "/Cluster"),
            GetMenuItemWithPath("Inspection", "cluster", "/Cluster/Inspection"),
            GetMenuItemWithPath("Metrics", "cluster", "/Cluster/Metrics"),
        };
    }

    private MenuDataItem GetMenuItem(string item, string icon)
    {
        return new MenuDataItem
        {
            Path = item,
            Name = item,
            Key  = item,
            Icon = icon,
        };
    }

    private MenuDataItem GetMenuItemWithPath(string item, string icon, string path)
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
            GetMenuItemWithPath(L["Pod"], "block", "/Pods"),
            GetMenuItemWithPath(L["Deployment"], "deployment-unit", "Deployments"),
            GetMenuItemWithPath(L["ReplicaSet"], "build", "/ReplicaSets"),
            GetMenuItemWithPath(L["DaemonSet"], "partition", "/DaemonSet"),
            GetMenuItemWithPath(L["StatefulSet"], "project", "/StatefulSet"),
            GetMenuItemWithPath(L["ReplicationController"], "split-cells", "/ReplicationController"),
            GetMenuItemWithPath(L["Job"], "container", "/Job"),
            GetMenuItemWithPath(L["CronJob"], "history", "/CronJob")
        };
    }

    private MenuDataItem[] ConfigMenu()
    {
        return new[]
        {
            GetMenuItemWithPath(L["ConfigMap"], "table", "/ConfigMap"),
            GetMenuItemWithPath(L["Secret"], "file-protect", "/Secret"),
            GetMenuItemWithPath(L["ResourceQuota"], "one-to-one", "/ResourceQuota"),
            GetMenuItemWithPath(L["LimitRange"], "column-width", "/LimitRange"),
            GetMenuItemWithPath(L["Lease"], "credit-card", "/Lease"),
            GetMenuItemWithPath(L["HorizontalPodAutoscaler"], "ungroup", "/HorizontalPodAutoscaler"),
            GetMenuItemWithPath(L["PodDisruptionBudget"], "appstore-add", "/PodDisruptionBudget"),
            GetMenuItemWithPath(L["PriorityClass"], "insert-row-left", "/PriorityClass"),
            GetMenuItemWithPath(L["ValidatingWebhookConfiguration"], "security-scan",
                "/ValidatingWebhookConfiguration"),
            GetMenuItemWithPath(L["MutatingWebhookConfiguration"], "sisternode", "/MutatingWebhookConfiguration")
        };
    }

    private MenuDataItem[] NetworkMenu()
    {
        return new[]
        {
            GetMenuItem("Service", "partition"),
            GetMenuItem("EndpointSlice", "format-painter"),
            GetMenuItem("Endpoints", "node-expand"),
            GetMenuItem("NetworkPolicy", "partition"),
            GetMenuItem("IngressClass", "reconciliation"),
            GetMenuItem("Ingress", "gateway"),
        };
    }

    private MenuDataItem[] StorageMenu()
    {
        return new[]
        {
            GetMenuItem("StorageClass", "file-sync"),
            GetMenuItemWithPath("PV", "file-done", "PersistentVolume"),
            GetMenuItemWithPath("PVC", "delivered-procedure", "PersistentVolumeClaim"),
        };
    }

    private MenuDataItem[] AccessControlMenu()
    {
        return new[]
        {
            GetMenuItem("ServiceAccount", "team"),
            GetMenuItem("ClusterRole", "audit"),
            GetMenuItem("ClusterRoleBinding", "api"),
            GetMenuItem("Role", "idcard"),
            GetMenuItem("RoleBinding", "contacts"),
        };
    }
}
