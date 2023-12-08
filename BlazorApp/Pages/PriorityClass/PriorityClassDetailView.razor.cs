using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.PriorityClass
{
    public partial class PriorityClassDetailView : FeedbackComponent<V1PriorityClass, bool>
    {
        private V1PriorityClass Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
