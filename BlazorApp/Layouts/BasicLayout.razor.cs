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

    [Inject] public IStringLocalizer L { get; set; }

    [Inject] private IAiService Ai { get; set; }

    protected override async Task OnInitializedAsync()
    {
        MenuData =
        [
            new MenuDataItem
            {
                Name = L["Cluster"],
                Key = "Cluster",
                Icon = "cluster",
                Children = ClusterMenu(),
            },
            new MenuDataItem
            {
                Path = "/PortForward",
                Name = L["PortForward"],
                Key = "PortForward",
                Icon = "pic-center",
                HideInMenu = KubernetesClientConfiguration.IsInCluster()
            },

            GetMenuItemWithPath(L["Namespace"], "book", "/Namespace"),
            GetMenuItemWithPath(L["Node"], "database", "/Node"),
            new MenuDataItem
            {
                Name = L["Workloads"],
                Key = "Workloads",
                Icon = "appstore",
                Children = WorkloadsMenu(),
            },
            new MenuDataItem
            {
                Name = L["Config"],
                Key = "Config",
                Icon = "control",
                Children = ConfigMenu(),
            },
            new MenuDataItem
            {
                Name = L["Network"],
                Key = "Network",
                Icon = "apartment",
                Children = NetworkMenu(),
            },
            new MenuDataItem
            {
                Name = L["Gateway"],
                Key = "Gateway",
                Icon = "Gateway",
                Children = GatewayMenu()
            },
            new MenuDataItem
            {
                Name = L["Storage"],
                Key = "Storage",
                Icon = "inbox",
                Children = StorageMenu(),
            },
            new MenuDataItem
            {
                Name = L["AccessControl"],
                Key = "AccessControl",
                Icon = "verified",
                Children = AccessControlMenu(),
            },
            GetMenuItemWithPath(L["CRD"], "code", "/Crd"),
            new MenuDataItem
            {
                Path = "/ChatDeploy",
                Name = L["ChatDeploy"],
                Key = "ChatDeploy",
                Icon = "robot",
                HideInMenu = !Ai.Enabled(),
            },
            new MenuDataItem
            {
                Path = "/Ai/Workflow",
                Name = L["AiWorkflow"],
                Key = "Workflow",
                Icon = "project",
                HideInMenu = !Ai.Enabled()
            },
            new MenuDataItem
            {
                Name = L["Settings"],
                Key = "Settings",
                Icon = "setting",
                Children = SettingMenu()
            },
            GetMenuItemWithPath(L["Example"], "unordered-list", "/Example"),
            GetMenuItemWithPath(L["OpenSource"], "github", "/OpenSource")
        ];
        await base.OnInitializedAsync();
    }

    private MenuDataItem[] GatewayMenu()
    {
        return new[]
        {
            GetMenuItemWithPath(L["GatewayClass"], "gateway", "/Gateway/GatewayClass"),
            GetMenuItemWithPath(L["Gateway"], "gateway", "/Gateway/Gateway"),
            GetMenuItemWithPath(L["HTTPRoute"], "gateway", "/Gateway/HTTPRoute"),
            GetMenuItemWithPath(L["GRPCRoute"], "gateway", "/Gateway/GRPCRoute"),
            GetMenuItemWithPath(L["TCPRoute"], "gateway", "/Gateway/TcpRoute"),
            GetMenuItemWithPath(L["UDPRoute"], "gateway", "/Gateway/UdpRoute"),
            // GetMenuItemWithPath(L["TLSRoute"], "gateway", "/Gateway/TlsRoute"),
            GetMenuItemWithPath(L["TLSPolicy"], "gateway", "/Gateway/BackendTLSPolicy"),
            // GetMenuItemWithPath(L["LBPolicy"], "gateway", "/Gateway/BackendLBPolicy"),
            GetMenuItemWithPath(L["ReferenceGrant"], "gateway", "/Gateway/ReferenceGrant"),
        };
    }

    private MenuDataItem[] SettingMenu()
    {
        return new[]
        {
            GetMenuItemWithPath(L["Language"], "translation", "/Settings/Language"),
            new MenuDataItem
            {
                Path = "/Settings/Run",
                Name = L["Run"],
                Key = "Run",
                Icon = "pic-center",
                HideInMenu = !KubernetesClientConfiguration.IsInCluster()
            },
        };
    }

    private MenuDataItem[] ClusterMenu()
    {
        return new[]
        {
            GetMenuItemWithPath(L["Overview"], "dashboard", "/Cluster"),
            GetMenuItemWithPath(L["Inspection"], "scan", "/Cluster/Inspection"),
            GetMenuItemWithPath(L["Metrics"], "cluster", "/Cluster/Metrics"),
            GetMenuItemWithPath(L["Diagram"], "deployment-unit", "/PodDiagram")
        };
    }

    private MenuDataItem GetMenuItem(string item, string icon)
    {
        return new MenuDataItem
        {
            Path = item,
            Name = item,
            Key = item,
            Icon = icon,
        };
    }

    private MenuDataItem GetMenuItemWithPath(string item, string icon, string path)
    {
        return new MenuDataItem
        {
            Path = path,
            Name = item,
            Key = item,
            Icon = icon,
        };
    }

    private MenuDataItem[] WorkloadsMenu()
    {
        return new[]
        {
            GetMenuItemWithPath(L["Pod"], "appstore", "/Pods"),
            GetMenuItemWithPath(L["Deployment"], "hdd", "Deployments"),
            GetMenuItemWithPath(L["ReplicaSet"], "build", "/ReplicaSets"),
            GetMenuItemWithPath(L["DaemonSet"], "reconciliation", "/DaemonSet"),
            GetMenuItemWithPath(L["StatefulSet"], "project", "/StatefulSet"),
            GetMenuItemWithPath(L["ReplicationController"], "split-cells", "/ReplicationController"),
            GetMenuItemWithPath(L["Job"], "container", "/Job"),
            GetMenuItemWithPath(L["CronJob"], "schedule", "/CronJob")
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
            GetMenuItemWithPath(L["Service"], "api", "/Service"),
            GetMenuItemWithPath(L["EndpointSlice"], "format-painter", "/EndpointSlice"),
            GetMenuItemWithPath(L["Endpoints"], "node-expand", "/Endpoints"),
            GetMenuItemWithPath(L["NetworkPolicy"], "partition", "/NetworkPolicy"),
            GetMenuItemWithPath(L["IngressClass"], "reconciliation", "/IngressClass"),
            GetMenuItemWithPath(L["Ingress"], "cloud", "/Ingress")
        };
    }

    private MenuDataItem[] StorageMenu()
    {
        return new[]
        {
            GetMenuItemWithPath(L["StorageClass"], "file-sync", "/StorageClass"),
            GetMenuItemWithPath(L["PersistentVolume"], "file-done", "PersistentVolume"),
            GetMenuItemWithPath(L["PersistentVolumeClaim"], "delivered-procedure", "PersistentVolumeClaim")
        };
    }

    private MenuDataItem[] AccessControlMenu()
    {
        return new[]
        {
            GetMenuItemWithPath(L["ServiceAccount"], "team", "/ServiceAccount"),
            GetMenuItemWithPath(L["ClusterRole"], "audit", "/ClusterRole"),
            GetMenuItemWithPath(L["ClusterRoleBinding"], "api", "/ClusterRoleBinding"),
            GetMenuItemWithPath(L["Role"], "idcard", "/Role"),
            GetMenuItemWithPath(L["RoleBinding"], "contacts", "/RoleBinding")
        };
    }
}
