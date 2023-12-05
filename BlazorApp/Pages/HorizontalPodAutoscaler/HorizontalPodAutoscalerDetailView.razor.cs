using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.HorizontalPodAutoscaler
{
    public partial class HorizontalPodAutoscalerDetailView : FeedbackComponent<V1HorizontalPodAutoscaler, bool>
    {
        private V1HorizontalPodAutoscaler Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
