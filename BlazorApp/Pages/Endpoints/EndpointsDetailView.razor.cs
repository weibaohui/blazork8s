using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.Endpoints
{
    public partial class EndpointsDetailView :  DrawerPageBase<V1Endpoints>
    {
        private V1Endpoints Endpoints { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Endpoints = base.Options;
            await base.OnInitializedAsync();
        }
    }
}