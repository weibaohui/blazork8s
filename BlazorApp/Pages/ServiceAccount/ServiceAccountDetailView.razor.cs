using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.ServiceAccount
{
    public partial class ServiceAccountDetailView : FeedbackComponent<V1ServiceAccount, bool>
    {
        private V1ServiceAccount Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
