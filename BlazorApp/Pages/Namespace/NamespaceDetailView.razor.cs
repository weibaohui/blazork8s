using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.Namespace
{
    public partial class NamespaceDetailView :  DrawerPageBase<V1Namespace>
    {
        private V1Namespace Namespace { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Namespace = base.Options;
            await base.OnInitializedAsync();
        }
    }
}