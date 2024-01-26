using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.PodDisruptionBudget
{
    public partial class PodDisruptionBudgetDetailView :  DrawerPageBase<V1PodDisruptionBudget>
    {
        private V1PodDisruptionBudget PodDisruptionBudget { get; set; }

        [Inject]
        private IPodService PodService { get; set; }
        private IList<V1Pod> PodList { get; set; } = new List<V1Pod>();

        protected override async Task OnInitializedAsync()
        {
            PodDisruptionBudget = base.Options;
            var matchLabels = PodDisruptionBudget?.Spec?.Selector?.MatchLabels;
            if (matchLabels != null)
            {
                var filter = PodSelectorHelper.ToFilter(matchLabels);
                PodList = await PodService.FilterPodByLabels(PodDisruptionBudget.Namespace(), filter);
            }
            await base.OnInitializedAsync();
        }
    }
}
