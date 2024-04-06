using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using Entity;

namespace BlazorApp.Pages.PortForwarding
{
    public partial class PortForwardDetailView : DrawerPageBase<PortForward>
    {
        private PortForward Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}