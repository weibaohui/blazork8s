using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.PersistentVolume
{
    public partial class PersistentVolumeDetailView : FeedbackComponent<V1PersistentVolume, bool>
    {
        private V1PersistentVolume Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
