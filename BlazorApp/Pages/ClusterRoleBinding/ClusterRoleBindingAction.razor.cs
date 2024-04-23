using AntDesign;
using BlazorApp.Pages.Common;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ClusterRoleBinding;

public partial class ClusterRoleBindingAction : PageBase
{
    [Parameter] public V1ClusterRoleBinding Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;
}