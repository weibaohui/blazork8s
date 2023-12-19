using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.EndpointSlice
{
    public partial class EndpointSliceDetailView :  DrawerPageBase<V1EndpointSlice>
    {
        private V1EndpointSlice EndpointSlice { get; set; }
        protected override async Task OnInitializedAsync()
        {
            EndpointSlice = base.Options;
            await base.OnInitializedAsync();
        }
    }
}