using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.ClusterRole
{
    public partial class ClusterRoleDetailView :  DrawerPageBase<V1ClusterRole>
    {
        private V1ClusterRole Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
