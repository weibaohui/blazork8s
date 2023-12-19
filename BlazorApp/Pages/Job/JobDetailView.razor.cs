using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.Job
{
    public partial class JobDetailView :  DrawerPageBase<V1Job>
    {
        private V1Job Job { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Job = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
