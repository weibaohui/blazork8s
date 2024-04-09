using System;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Pages.Workload;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.${Item};

public partial class ${Item}Action : PageBase
{
    [Parameter]
    public ${ItemType} Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

}
