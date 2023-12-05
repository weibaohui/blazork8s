using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.LimitRange
{
    public partial class LimitRangeDetailView : FeedbackComponent<V1LimitRange, bool>
    {
        private V1LimitRange Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
