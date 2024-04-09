using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Job;

public partial class JobSpecView :ComponentBase
{

    [Parameter]
    public V1JobSpec JobSpec { get; set; }
}
