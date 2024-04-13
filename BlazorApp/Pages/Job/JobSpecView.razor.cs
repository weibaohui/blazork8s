using BlazorApp.Pages.Common;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Job;

public partial class JobSpecView : PageBase
{
    [Parameter] public V1JobSpec JobSpec { get; set; }
}
