using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Role
{
    public partial class RoleDetailView : DrawerPageBase<V1Role>
    {
        [Inject]
        public IRoleBindingService RoleBindingService { get; set; }


        private List<V1Subject> Subjects { get; set; }
        private V1Role          Role     { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Role = base.Options;
            Subjects= ListServiceAccount();
            await base.OnInitializedAsync();
        }


        private List<V1Subject> ListServiceAccount()
        {
            var bindings = RoleBindingService.List()
                .Where(x =>
                    x.Namespace()== Role.Namespace() &&
                    x.RoleRef is not null && x.RoleRef.Name == Role.Name()
                )
                .ToList();
            return bindings.SelectMany(x => x.Subjects).ToList();
        }
    }
}
