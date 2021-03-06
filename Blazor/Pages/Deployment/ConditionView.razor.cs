using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Deployment
{
    public partial class ConditionView:ComponentBase
    {
        [Parameter]
        public IList<V1DeploymentCondition> ConditionList { get; set; }
    }
}
