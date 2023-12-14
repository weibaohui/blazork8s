using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.PriorityClass
{
    public partial class PriorityClassDetailView :  DrawerPageBase<V1PriorityClass>
    {
        private V1PriorityClass Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
