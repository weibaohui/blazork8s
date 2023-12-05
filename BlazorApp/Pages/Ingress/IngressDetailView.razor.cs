using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.Ingress
{
    public partial class IngressDetailView : FeedbackComponent<V1Ingress, bool>
    {
        private V1Ingress Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
