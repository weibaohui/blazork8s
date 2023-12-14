using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.HorizontalPodAutoscaler
{
    public partial class HorizontalPodAutoscalerDetailView :  DrawerPageBase<V1HorizontalPodAutoscaler>
    {
        private V1HorizontalPodAutoscaler Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
