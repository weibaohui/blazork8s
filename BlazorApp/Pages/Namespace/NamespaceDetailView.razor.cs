using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Namespace
{
    public partial class NamespaceDetailView : DrawerPageBase<V1Namespace>
    {
        [Inject] private IPodService PodService { get; set; }

        private IList<V1Pod> PodList { get; set; } = new List<V1Pod>();
        private V1Namespace Namespace { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Namespace = base.Options;
            PodList = PodService.ListByNamespace(Namespace.Name());
            await base.OnInitializedAsync();
        }
    }
}