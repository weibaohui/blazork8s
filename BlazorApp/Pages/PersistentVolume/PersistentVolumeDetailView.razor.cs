using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.PersistentVolume
{
    public partial class PersistentVolumeDetailView :  DrawerPageBase<V1PersistentVolume>
    {
        private V1PersistentVolume PersistentVolume { get; set; }
        protected override async Task OnInitializedAsync()
        {
            PersistentVolume = base.Options;
            await base.OnInitializedAsync();
        }
    }
}