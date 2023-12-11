using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ClusterRoleBinding
{
    public partial class ClusterRoleBindingDetailView : FeedbackComponent<V1ClusterRoleBinding, bool>
    {
        private V1ClusterRoleBinding Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
