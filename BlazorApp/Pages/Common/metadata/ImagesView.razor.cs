using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.metadata;

public partial class ImagesView : ComponentBase
{
    [Parameter]
    public V1PodSpec PodSpec { get; set; }
}
