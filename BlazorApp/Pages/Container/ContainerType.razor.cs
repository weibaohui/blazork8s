using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace  BlazorApp.Pages.Container
{
    public partial class ContainerType : PageBase
    {

        [Parameter]
        public string Type { get; set; }
    }
}
