using System;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.TableModels;
using BlazorApp.Service;
using BlazorApp.Service.impl;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Deployment;

public partial class DeploymentIndex : ComponentBase
{
    [Inject]
    private IDeploymentService DeploymentService { get; set; }

    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    public TableDataHelper<V1Deployment> tps = new();


    private string                            _selectedNs = "";
    private ResourceCacheHelper<V1Deployment> _itemList   = ResourceCacheHelper<V1Deployment>.Instance();

    private async Task OnResourceChanged(ResourceCacheHelper<V1Deployment> data)
    {
        _itemList = data;
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnInitializedAsync()
    {
        await tps.GetData(_selectedNs);
    }


    public async Task OnNsSelectedHandler(string ns)
    {
        _selectedNs = ns;
        await tps.OnNsSelectedHandler(ns);
        await InvokeAsync(StateHasChanged);
    }

    public void RemoveSelection(string uid)
    {
        tps.SelectedRows = tps.SelectedRows.Where(x => x.Metadata.Uid != uid);
    }

    private void Delete(string uid)
    {
    }

    private async Task OnChange(QueryModel<V1Deployment> queryModel)
    {
        tps.OnChange(queryModel);
        await InvokeAsync(StateHasChanged);
    }

    private async Task OnSearchHandler(string key)
    {
        tps.SearchName(key);

        await InvokeAsync(StateHasChanged);
    }


    private async Task OnDeployClick(V1Deployment deploy)
    {
        var options = PageDrawerService.DefaultOptions($"{deploy.Kind ?? "Deployment"}:{deploy.Name()}");
        await PageDrawerService.ShowDrawerAsync<DeploymentDetailView, V1Deployment, bool>(options, deploy);
    }
}