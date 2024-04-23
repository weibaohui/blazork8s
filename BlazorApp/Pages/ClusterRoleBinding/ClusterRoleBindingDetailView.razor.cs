using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.ClusterRoleBinding;

public partial class ClusterRoleBindingDetailView : DrawerPageBase<V1ClusterRoleBinding>
{
    private V1ClusterRoleBinding ClusterRoleBinding { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ClusterRoleBinding = base.Options;
        await base.OnInitializedAsync();
    }
}