using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ClusterRole;

public partial class ClusterRoleDetailView : DrawerPageBase<V1ClusterRole>
{
    [Inject]
    public IClusterRoleService ClusterRoleService { get; set; }

    private V1ClusterRole ClusterRole { get; set; }

    private IList<Rbacv1Subject> Subjects { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ClusterRole = Options;
        Subjects    = ClusterRoleService.ListManagedSubjectByClusterRole(ClusterRole);
        await base.OnInitializedAsync();
    }
}