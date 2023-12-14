using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.RoleBinding
{
    public partial class RoleBindingDetailView :  DrawerPageBase<V1RoleBinding>
    {
        private V1RoleBinding Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
