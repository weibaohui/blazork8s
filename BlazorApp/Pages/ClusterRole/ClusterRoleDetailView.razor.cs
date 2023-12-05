using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.ClusterRole
{
    public partial class ClusterRoleDetailView : FeedbackComponent<V1ClusterRole, bool>
    {
        private V1ClusterRole Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
