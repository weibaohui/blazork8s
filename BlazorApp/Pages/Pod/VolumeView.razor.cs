using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod
{
    public partial class VolumeView : ComponentBase
    {
        [Parameter]
        public V1PodSpec PodSpec { get; set; }

        [Parameter]
        public string Namespace { get; set; }
    }
}
