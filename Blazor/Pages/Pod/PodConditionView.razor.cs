using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using Blazor.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Pod
{
    public partial class PodConditionView : ComponentBase
    {
        [Parameter]
        public IList<V1PodCondition> Conditions { get; set; }
    }
}
