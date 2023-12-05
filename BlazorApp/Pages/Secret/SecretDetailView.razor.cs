using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.Secret
{
    public partial class SecretDetailView : FeedbackComponent<V1Secret, bool>
    {
        private V1Secret Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
