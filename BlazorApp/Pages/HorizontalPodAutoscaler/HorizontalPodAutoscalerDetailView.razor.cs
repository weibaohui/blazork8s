using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.HorizontalPodAutoscaler
{
    public partial class HorizontalPodAutoscalerDetailView :  DrawerPageBase<V1HorizontalPodAutoscaler>
    {
        private V1HorizontalPodAutoscaler HorizontalPodAutoscaler { get; set; }
        protected override async Task OnInitializedAsync()
        {
            HorizontalPodAutoscaler = base.Options;
            await base.OnInitializedAsync();
        }
    }
}