using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.Role
{
    public partial class RoleDetailView : FeedbackComponent<V1Role, bool>
    {
        private V1Role Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
