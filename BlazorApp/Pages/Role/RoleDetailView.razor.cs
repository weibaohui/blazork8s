using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.Role
{
    public partial class RoleDetailView :  DrawerPageBase<V1Role>
    {
        private V1Role Role { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Role = base.Options;
            await base.OnInitializedAsync();
        }
    }
}