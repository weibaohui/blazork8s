using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace  BlazorApp.Pages.ReplicaSet;

public partial class Condition:ComponentBase
{
    [Parameter]
    public IList<V1ReplicaSetCondition> ConditionList { get; set; }
}
