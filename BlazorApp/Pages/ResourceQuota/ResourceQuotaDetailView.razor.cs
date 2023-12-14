using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.ResourceQuota
{
    public partial class ResourceQuotaDetailView :  DrawerPageBase<V1ResourceQuota>
    {
        private V1ResourceQuota Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
