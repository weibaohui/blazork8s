using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.RoleBinding
{
    public partial class RoleBindingDetailView :  DrawerPageBase<V1RoleBinding>
    {
        private V1RoleBinding RoleBinding { get; set; }
        protected override async Task OnInitializedAsync()
        {
            RoleBinding = base.Options;
            await base.OnInitializedAsync();
        }
    }
}