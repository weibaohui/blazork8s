using System.Threading.Tasks;
using AntDesign;
using Entity;

namespace BlazorApp.Pages.PortForwarding
{
    public partial class PortForwardDetailView : FeedbackComponent<PortForward, bool>
    {
        private PortForward Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
