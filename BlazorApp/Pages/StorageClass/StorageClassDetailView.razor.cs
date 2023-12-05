using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.StorageClass
{
    public partial class StorageClassDetailView : FeedbackComponent<V1StorageClass, bool>
    {
        private V1StorageClass Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
