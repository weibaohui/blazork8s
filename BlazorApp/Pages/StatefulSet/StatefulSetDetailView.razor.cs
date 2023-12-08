using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.StatefulSet
{
    public partial class StatefulSetDetailView : FeedbackComponent<V1StatefulSet, bool>
    {
        private V1StatefulSet Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
