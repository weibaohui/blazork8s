using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Deployment;

public partial class DeploymentIndex : TableBase<V1Deployment>
{
    [Inject]
    private IDeploymentService DeploymentService { get; set; }


    private async Task OnResourceChanged(ResourceCache<V1Deployment> data)
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


    private async Task OnDeployClick(V1Deployment deploy)
    {
        await PageDrawerHelper<V1Deployment>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<DeploymentDetailView, V1Deployment, bool>(deploy);

    }
}
