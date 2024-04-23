using AntDesign;
using BlazorApp.Pages.Common;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Ingress;

public partial class IngressAction : PageBase
{
    [Parameter] public V1Ingress Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;
}