using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.ReplicationController
{
    public partial class ReplicationControllerDetailView :  DrawerPageBase<V1ReplicationController>
    {
        private V1ReplicationController ReplicationController { get; set; }
        protected override async Task OnInitializedAsync()
        {
            ReplicationController = base.Options;
            await base.OnInitializedAsync();
        }
    }
}