using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.RoleBinding;

public partial class RoleBindingAction : PageBase
{
    [Parameter]
    public V1RoleBinding Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; } = MenuMode.Vertical;
}
