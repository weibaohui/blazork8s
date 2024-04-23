using BlazorApp.Pages.Common;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Container;

public partial class ProbeView : PageBase
{
    [Parameter] public V1Probe Probe { get; set; }

    [Parameter] public string ProbeType { get; set; }
}