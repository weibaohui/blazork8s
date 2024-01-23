using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod
{
    public partial class PodDetailView : DrawerPageBase<V1Pod>
    {
        [Inject]
        private IMetricsService MetricsService { get; set; }

        public  V1Pod Item;
        private bool  _isMetricsServerReady;



        protected override async Task OnInitializedAsync()
        {
            Item                 = base.Options;
            _isMetricsServerReady = await MetricsService.MetricsServerReady();
            await base.OnInitializedAsync();
        }
    }
}
