using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Common.Metadata;

public partial class ImagesView : PageBase
{
    [Parameter]
    public V1PodSpec PodSpec { get; set; }
}
