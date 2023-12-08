using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

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
