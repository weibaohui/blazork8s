using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.NetworkPolicy
{
    public partial class NetworkPolicyDetailView : FeedbackComponent<V1NetworkPolicy, bool>
    {
        private V1NetworkPolicy Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
