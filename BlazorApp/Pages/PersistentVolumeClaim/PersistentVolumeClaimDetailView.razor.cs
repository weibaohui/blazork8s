using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.PersistentVolumeClaim
{
    public partial class PersistentVolumeClaimDetailView :  DrawerPageBase<V1PersistentVolumeClaim>
    {
        private V1PersistentVolumeClaim PersistentVolumeClaim { get; set; }
        protected override async Task OnInitializedAsync()
        {
            PersistentVolumeClaim = base.Options;
            await base.OnInitializedAsync();
        }
    }
}