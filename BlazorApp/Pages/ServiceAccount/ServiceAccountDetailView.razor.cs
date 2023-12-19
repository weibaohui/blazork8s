using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
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