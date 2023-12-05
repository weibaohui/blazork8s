using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.ClusterRoleBinding
{
    public partial class ClusterRoleBindingDetailView : FeedbackComponent<V1ClusterRoleBinding, bool>
    {
        private V1ClusterRoleBinding Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
