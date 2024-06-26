using System.Collections.Generic;
using BlazorApp.Pages.Common;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ReplicaSet;

public partial class ConditionView : PageBase
{
    [Parameter] public IList<V1ReplicaSetCondition> ConditionList { get; set; }
    [Parameter] public string ExplainField { get; set; }
}