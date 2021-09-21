using System.Collections.Generic;
using AntDesign;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Workload
{
    public partial class ImagesView :ComponentBase
    {


        [Parameter]
        public V1PodSpec PodSpec { get; set; }
    }
}
