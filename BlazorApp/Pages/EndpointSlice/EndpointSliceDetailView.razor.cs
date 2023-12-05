using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.EndpointSlice
{
    public partial class EndpointSliceDetailView : FeedbackComponent<V1EndpointSlice, bool>
    {
        private V1EndpointSlice Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
