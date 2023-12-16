using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.DaemonSet
{
    public partial class DaemonSetDetailView :  DrawerPageBase<V1DaemonSet>
    {
        private V1DaemonSet DaemonSet { get; set; }

        protected override async Task OnInitializedAsync()
        {
            DaemonSet = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
