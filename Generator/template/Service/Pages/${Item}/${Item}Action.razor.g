using AntDesign;
using Entity.Crd.Gateway;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.Gateway.${Item};

public partial class ${Item}Action : PageBase
{
    [Parameter]
    public ${ItemType} Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

}
