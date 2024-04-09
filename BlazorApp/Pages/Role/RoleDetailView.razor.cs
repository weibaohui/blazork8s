using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Role
{
    public partial class RoleDetailView : DrawerPageBase<V1Role>
    {
        [Inject]
        public IRoleService RoleService { get; set; }


        private IList<Rbacv1Subject> Subjects { get; set; }
        private V1Role               Role     { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Role = base.Options;
            Subjects= RoleService.ListManagedSubjectByRole(Role);
            await base.OnInitializedAsync();
        }


    }
}
