using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ServiceAccount
{
    public partial class ServiceAccountDetailView : DrawerPageBase<V1ServiceAccount>
    {
        [Inject]
        public IRoleService RoleService { get; set; }

        [Inject]
        public IRoleBindingService RoleBindingService { get; set; }

        private V1ServiceAccount ServiceAccount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ServiceAccount = base.Options;
            await base.OnInitializedAsync();
        }


        private List<V1RoleRef> ListSaRoles()
        {
            var saName = ServiceAccount.Name();
            var bindings = RoleBindingService.List()
                .Where(x =>
                    x.Subjects is { Count: > 0 }
                    && x.Subjects.Any(y => y.Kind == "ServiceAccount" && y.Name == saName)
                )
                .ToList();
            return bindings.Select(x => x.RoleRef).ToList();
        }
    }
}
