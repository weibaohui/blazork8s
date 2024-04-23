using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Crd;

public partial class CrdIndex : TableBase<V1CustomResourceDefinition>
{
    [Inject] public IKubeService KubeService { get; set; }

    [Inject] private ICustomResourceDefinitionService CrdService { get; set; }


    public IDictionary<string, int> CrCount { get; set; } = new Dictionary<string, int>(); // <name,count>

    private async Task OnResourceChanged(ResourceCache<V1CustomResourceDefinition> data)
    {
        ItemList = data;
        TableData.CopyData(ItemList);
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnInitializedAsync()
    {
        TableData.CopyData(ItemList);
        await CalcCrCount(ItemList.Get());
        await base.OnInitializedAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task CalcCrCount(IEnumerable<V1CustomResourceDefinition> crds)
    {
        foreach (var crd in crds)
        {
            var crInstanceList = await CrdService.GetCrInstanceList(crd);
            CrCount.Add(crd.Name(), crInstanceList.Count);
        }
    }

    private async Task OnItemNameClick(V1CustomResourceDefinition item)
    {
        await PageDrawerHelper<V1CustomResourceDefinition>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<CrdDetailView, V1CustomResourceDefinition, bool>(item);
    }
}