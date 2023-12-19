using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.PodDisruptionBudget
{
    public partial class PodDisruptionBudgetDetailView :  DrawerPageBase<V1PodDisruptionBudget>
    {
        private V1PodDisruptionBudget PodDisruptionBudget { get; set; }
        protected override async Task OnInitializedAsync()
        {
            PodDisruptionBudget = base.Options;
            await base.OnInitializedAsync();
        }
    }
}