using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ClusterRole
{
    public partial class ClusterRoleDetailView : DrawerPageBase<V1ClusterRole>
    {
        [Inject]
        public IClusterRoleBindingService ClusterRoleBindingService { get; set; }

        private V1ClusterRole ClusterRole { get; set; }

        private List<V1Subject> Subjects { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ClusterRole = base.Options;
            Subjects    = ListServiceAccount();
            await base.OnInitializedAsync();
        }


        private List<V1Subject> ListServiceAccount()
        {
            var name = ClusterRole.Name();
            var bindings = ClusterRoleBindingService.List()
                .Where(x =>
                    x.RoleRef is not null && x.RoleRef.Name == name
                )
                .ToList();
            return bindings.SelectMany(x => x.Subjects).ToList();
        }
    }
}
