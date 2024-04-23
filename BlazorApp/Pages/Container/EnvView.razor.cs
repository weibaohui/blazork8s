using System.Collections.Generic;
using BlazorApp.Pages.Common;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Container;

public partial class EnvView : PageBase
{
    [Parameter] public IList<V1EnvVar> Env { get; set; }
}