using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.Secret
{
    public partial class SecretDetailView :  DrawerPageBase<V1Secret>
    {
        private V1Secret Secret { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Secret = base.Options;
            await base.OnInitializedAsync();
        }
    }
}