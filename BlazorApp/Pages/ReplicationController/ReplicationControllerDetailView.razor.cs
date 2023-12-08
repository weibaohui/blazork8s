using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.ReplicationController
{
    public partial class ReplicationControllerDetailView : FeedbackComponent<V1ReplicationController, bool>
    {
        private V1ReplicationController Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
