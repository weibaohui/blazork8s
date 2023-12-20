using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace  BlazorApp.Pages.Deployment;

public partial class DeploymentCondition:ComponentBase
{
    [Parameter]
    public IList<V1DeploymentCondition> ConditionList { get; set; }
}
