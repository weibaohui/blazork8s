using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.NetworkPolicy
{
    public partial class NetworkPolicyDetailView :  DrawerPageBase<V1NetworkPolicy>
    {
        private V1NetworkPolicy NetworkPolicy { get; set; }
        protected override async Task OnInitializedAsync()
        {
            NetworkPolicy = base.Options;
            await base.OnInitializedAsync();
        }
    }
}