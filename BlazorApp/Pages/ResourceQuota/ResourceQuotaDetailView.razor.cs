using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.ResourceQuota
{
    public partial class ResourceQuotaDetailView : FeedbackComponent<V1ResourceQuota, bool>
    {
        private V1ResourceQuota Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
