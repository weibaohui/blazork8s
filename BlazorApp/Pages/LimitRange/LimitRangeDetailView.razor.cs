using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.LimitRange
{
    public partial class LimitRangeDetailView :  DrawerPageBase<V1LimitRange>
    {
        private V1LimitRange LimitRange { get; set; }
        protected override async Task OnInitializedAsync()
        {
            LimitRange = base.Options;
            await base.OnInitializedAsync();
        }
    }
}