using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.PodDisruptionBudget
{
    public partial class PodDisruptionBudgetDetailView : FeedbackComponent<V1PodDisruptionBudget, bool>
    {
        private V1PodDisruptionBudget Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
