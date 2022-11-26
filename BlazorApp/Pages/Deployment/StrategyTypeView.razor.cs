using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace  BlazorApp.Pages.Deployment
{
    public partial class StrategyTypeView:ComponentBase
    {
        [Parameter]
        public V1DeploymentStrategy Strategy { get; set; }
    }
}
