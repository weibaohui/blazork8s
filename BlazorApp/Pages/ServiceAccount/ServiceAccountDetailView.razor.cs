using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.ServiceAccount
{
    public partial class ServiceAccountDetailView :  DrawerPageBase<V1ServiceAccount>
    {
        private V1ServiceAccount ServiceAccount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ServiceAccount = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
