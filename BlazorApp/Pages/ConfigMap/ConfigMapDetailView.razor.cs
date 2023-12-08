using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.ConfigMap
{
    public partial class ConfigMapDetailView : FeedbackComponent<V1ConfigMap, bool>
    {
        private V1ConfigMap Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
