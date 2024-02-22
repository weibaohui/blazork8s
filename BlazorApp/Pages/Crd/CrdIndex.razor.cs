using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Crd;

public partial class CrdIndex : TableBase<V1CustomResourceDefinition>
{
    [Inject]
    public IKubeService KubeService { get; set; }

    [Inject]
    private ICustomResourceDefinitionService CustomResourceDefinitionService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1CustomResourceDefinition> data)
    {
        ItemList = data;
        TableData.CopyData(ItemList);
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        TableData.CopyData(ItemList);
        await InvokeAsync(StateHasChanged);
    }
    //
    // private async Task<string> GetCrCount(V1CustomResourceDefinition context)
    // {
    //     var ret = string.Empty;
    //
    //     // var ret      = new Dictionary<string, int>();
    //     var group    = context.Spec.Group;
    //     var versions = context.Spec.Versions;
    //     var plural   = context.Spec.Names.Plural;
    //
    //     foreach (var v in versions)
    //     {
    //         var generic = new GenericClient(KubeService.Client(), group, v.Name, plural);
    //         var list    = await generic.ListAsync<CustomResourceList<CustomResource>>();
    //         // ret.Add(v.Name, list.Items.Count);
    //         ret += $"{v.Name}={list.Items.Count}, ";
    //     }
    //
    //     //
    //     // return ret;
    //     return ret;
    // }

    private async Task OnItemNameClick(V1CustomResourceDefinition item)
    {
        await PageDrawerHelper<V1CustomResourceDefinition>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<CrdDetailView, V1CustomResourceDefinition, bool>(item);
    }


}
