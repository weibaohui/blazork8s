using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.ConfigMap
{
    public partial class ConfigMapDetailView :  DrawerPageBase<V1ConfigMap>
    {
        private V1ConfigMap Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
