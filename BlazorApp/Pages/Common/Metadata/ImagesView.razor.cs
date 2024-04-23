using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class ImagesView : PageBase
{
    [Parameter] public V1PodSpec PodSpec { get; set; }
}