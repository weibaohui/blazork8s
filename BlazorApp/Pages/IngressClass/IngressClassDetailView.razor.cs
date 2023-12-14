using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.IngressClass
{
    public partial class IngressClassDetailView :  DrawerPageBase<V1IngressClass>
    {
        private V1IngressClass Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
