using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.ClusterRoleBinding
{
    public partial class ClusterRoleBindingDetailView :  DrawerPageBase<V1ClusterRoleBinding>
    {
        private V1ClusterRoleBinding Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
