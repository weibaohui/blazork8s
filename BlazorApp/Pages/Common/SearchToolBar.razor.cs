using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorApp.Pages.Common;

public partial class SearchToolBar<TItem> : ComponentBase where TItem : IKubernetesObject<V1ObjectMeta>
{
    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public int Count { get; set; }

    [Parameter]
    public bool HideNsSelector { get; set; } = false;

    [Parameter]
    public EventCallback<string> OnNsSelected { get; set; }

    [Parameter]
    public EventCallback<string> OnSearch { get; set; }

    [Parameter]
    public EventCallback OnRemoveAllClicked { get; set; }

    [Parameter]
    public EventCallback<string> OnSelectedItemCloseClicked { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public TableData<TItem> TableData { get; set; }

    [Inject]
    private IResourceCrudService ResourceCrudService { get; set; }

    private string TxtValue { get; set; }
    private string _style = " padding: 8px 0;";

    protected override async Task OnInitializedAsync()
    {
        TxtValue = TableData.GetCurrentSearchKey();
        await base.OnInitializedAsync();
    }

    private void OnNsSelectedHandler(string ns)
    {
        OnNsSelected.InvokeAsync(ns);
    }

    private async Task OnSearchHandler()
    {
        await OnSearch.InvokeAsync(TxtValue);
    }

    private async Task RemoveAllSelectedHandler()
    {
        await OnRemoveAllClicked.InvokeAsync();
    }


    private async Task CloseSelectedItemHandler(string nsAndName)
    {
        await OnSelectedItemCloseClicked.InvokeAsync(nsAndName);
    }

    private async Task DeleteHandler()
    {
        foreach (var item in TableData.SelectedRows)
        {
            // Console.WriteLine($"DeleteHandler 删除 {item.Kind} {item.Namespace()}/{item.Name()}");
            await ResourceCrudService.DeleteItem(item.Kind, item.Namespace(), item.Name());
            await OnSelectedItemCloseClicked.InvokeAsync($"{item.Namespace()}/{item.Name()}");
        }
    }
}
