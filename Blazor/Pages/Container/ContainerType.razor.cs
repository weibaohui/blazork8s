using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Container
{
    public partial class ContainerType : ComponentBase
    {

        [Parameter]
        public string Type { get; set; }
    }
}
