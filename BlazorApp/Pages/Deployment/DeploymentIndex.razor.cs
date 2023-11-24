using System;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.TableModels;
using BlazorApp.Pages.Common;
using BlazorApp.Service;
using BlazorApp.Service.impl;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Deployment;

public partial class DeploymentIndex : TableBase<V1Deployment>
{
    [Inject]
    private IDeploymentService DeploymentService { get; set; }

    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }


    private async Task OnResourceChanged(ResourceCacheHelper<V1Deployment> data)
    {
        ItemList = data;
        TableDataHelper.CopyData(ItemList);
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        TableDataHelper.CopyData(ItemList);
        await InvokeAsync(StateHasChanged);
    }


    private async Task OnDeployClick(V1Deployment deploy)
    {
        var options = PageDrawerService.DefaultOptions($"{deploy.Kind ?? "Deployment"}:{deploy.Name()}");
        await PageDrawerService.ShowDrawerAsync<DeploymentDetailView, V1Deployment, bool>(options, deploy);
    }
}
