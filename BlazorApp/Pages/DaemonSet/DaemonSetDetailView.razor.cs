using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.DaemonSet
{
    public partial class DaemonSetDetailView : FeedbackComponent<V1DaemonSet, bool>
    {
        private V1DaemonSet Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
