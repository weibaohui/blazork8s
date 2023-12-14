using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.LimitRange
{
    public partial class LimitRangeDetailView :  DrawerPageBase<V1LimitRange>
    {
        private V1LimitRange Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
