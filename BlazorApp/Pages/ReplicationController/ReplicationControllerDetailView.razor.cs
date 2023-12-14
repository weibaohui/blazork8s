using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.ReplicationController
{
    public partial class ReplicationControllerDetailView :  DrawerPageBase<V1ReplicationController>
    {
        private V1ReplicationController Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
