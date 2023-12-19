using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
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