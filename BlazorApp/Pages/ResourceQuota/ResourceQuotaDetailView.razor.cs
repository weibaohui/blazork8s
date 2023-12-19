using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.ResourceQuota
{
    public partial class ResourceQuotaDetailView :  DrawerPageBase<V1ResourceQuota>
    {
        private V1ResourceQuota ResourceQuota { get; set; }
        protected override async Task OnInitializedAsync()
        {
            ResourceQuota = base.Options;
            await base.OnInitializedAsync();
        }
    }
}