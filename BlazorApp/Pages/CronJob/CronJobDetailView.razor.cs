using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.CronJob
{
    public partial class CronJobDetailView :  DrawerPageBase<V1CronJob>
    {
        private V1CronJob Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
