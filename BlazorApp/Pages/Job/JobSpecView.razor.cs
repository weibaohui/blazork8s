using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Job;

public partial class JobSpecView :ComponentBase
{

    [Parameter]
    public V1JobSpec JobSpec { get; set; }
}
