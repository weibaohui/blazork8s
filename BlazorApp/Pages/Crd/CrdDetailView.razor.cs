using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service.k8s;
using Entity.Crd;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Crd
{
    public partial class CrdDetailView : DrawerPageBase<V1CustomResourceDefinition>
    {
        [Inject]
        public IKubeService KubeService { get; set; }

        [Inject]
        public ICustomResourceDefinitionService CrdService { get; set; }

        private V1CustomResourceDefinition CustomResourceDefinition { get; set; }

        private List<CustomResource> _crInstanceList = new List<CustomResource>();

        protected override async Task OnInitializedAsync()
        {
            CustomResourceDefinition = base.Options;

            _crInstanceList = await CrdService.GetCrInstanceList(CustomResourceDefinition);

            await base.OnInitializedAsync();
        }


        private async Task OnYamlClick(CustomResource cr)
        {
            var item = await CrdService.GetCrInstanceWithSpec(CustomResourceDefinition, cr);

            var options = PageDrawerService.DefaultOptions($"Yaml:{cr.Name()}", width: 1000);
            await PageDrawerService
                .ShowDrawerAsync<YamlView<object>, object, bool>(options, item);
        }
    }
}
