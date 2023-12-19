using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;

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