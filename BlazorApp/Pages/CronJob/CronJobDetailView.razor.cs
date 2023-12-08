using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.CronJob
{
    public partial class CronJobDetailView : FeedbackComponent<V1CronJob, bool>
    {
        private V1CronJob Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
