using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Container
{
    public partial class EnvView : ComponentBase
    {


        [Parameter]
        public IList<V1EnvVar> Env { get; set; }

    }
}
