using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.RoleBinding
{
    public partial class RoleBindingDetailView : FeedbackComponent<V1RoleBinding, bool>
    {
        private V1RoleBinding Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
