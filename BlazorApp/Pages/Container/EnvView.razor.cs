using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace  BlazorApp.Pages.Container
{
    public partial class EnvView : PageBase
    {


        [Parameter]
        public IList<V1EnvVar> Env { get; set; }

    }
}
