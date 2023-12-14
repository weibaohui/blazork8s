using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.PersistentVolume
{
    public partial class PersistentVolumeDetailView :  DrawerPageBase<V1PersistentVolume>
    {
        private V1PersistentVolume Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
