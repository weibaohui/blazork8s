using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.Crd
{
    public partial class CrdDetailView :  DrawerPageBase<V1CustomResourceDefinition>
    {
        private V1CustomResourceDefinition CustomResourceDefinition { get; set; }
        protected override async Task OnInitializedAsync()
        {
            CustomResourceDefinition = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
