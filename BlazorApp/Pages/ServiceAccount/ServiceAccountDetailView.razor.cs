using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.ServiceAccount
{
    public partial class ServiceAccountDetailView : DrawerPageBase<V1ServiceAccount>
    {
        [Inject]
        public IServiceAccountService ServiceAccountService { get; set; }


        private V1ServiceAccount ServiceAccount { get; set; }

        private IList<V1RoleRef> RoleRefs { get; set; }
        private IList<V1RoleRef> ClusterRoleRefs { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ServiceAccount  = base.Options;
            RoleRefs        = ServiceAccountService.ListRoles(ServiceAccount.Name());
            ClusterRoleRefs = ServiceAccountService.ListClusterRoles(ServiceAccount.Name());
            await base.OnInitializedAsync();
        }

    }
}
