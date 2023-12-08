using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.Endpoints
{
    public partial class EndpointsDetailView : FeedbackComponent<V1Endpoints, bool>
    {
        private V1Endpoints Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
