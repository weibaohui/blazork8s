using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Pod
{
    public partial class VolumeView : PageBase
    {
        [Parameter]
        public V1PodSpec PodSpec { get; set; }

        [Parameter]
        public string Namespace { get; set; }
    }
}
