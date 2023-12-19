using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.CronJob
{
    public partial class CronJobDetailView :  DrawerPageBase<V1CronJob>
    {
        private V1CronJob CronJob { get; set; }
        protected override async Task OnInitializedAsync()
        {
            CronJob = base.Options;
            await base.OnInitializedAsync();
        }
    }
}