using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.StorageClass
{
    public partial class StorageClassDetailView :  DrawerPageBase<V1StorageClass>
    {
        private V1StorageClass StorageClass { get; set; }
        protected override async Task OnInitializedAsync()
        {
            StorageClass = base.Options;
            await base.OnInitializedAsync();
        }
    }
}