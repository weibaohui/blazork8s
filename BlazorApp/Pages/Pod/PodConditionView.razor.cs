using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace  BlazorApp.Pages.Pod
{
    public partial class PodConditionView : ComponentBase
    {
        [Parameter]
        public IList<V1PodCondition> Conditions { get; set; }
    }
}
