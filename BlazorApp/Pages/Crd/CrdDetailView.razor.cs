using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service.k8s;
using Entity.Crd;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;
using Microsoft.Extensions.Localization;

namespace BlazorApp.Pages.Crd;

public partial class CrdDetailView : DrawerPageBase<V1CustomResourceDefinition>
{
    private List<CustomResource> _crInstanceList = new();



    [Inject]
    public IKubeService KubeService { get; set; }

    [Inject]
    public ICustomResourceDefinitionService CrdService { get; set; }

    private V1CustomResourceDefinition CustomResourceDefinition { get; set; }

    protected override async Task OnInitializedAsync()
    {
        CustomResourceDefinition = Options;

        _crInstanceList = await CrdService.GetCrInstanceList(CustomResourceDefinition);

        await base.OnInitializedAsync();
    }


    private async Task OnYamlClick(CustomResource cr)
    {
        var item = await CrdService.GetCrInstanceWithSpec(CustomResourceDefinition, cr);

        var options = PageDrawerService.DefaultOptions($"Yaml:{cr.Name()}", 1000);
        await PageDrawerService
            .ShowDrawerAsync<YamlView<object>, object, bool>(options, item);
    }
}
