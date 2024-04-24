using System.Linq;
using System.Threading.Tasks;
using AntDesign.TableModels;
using BlazorApp.Utils;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.Common;

public partial class TableBase<T> : PageBase where T : IKubernetesObject<V1ObjectMeta>
{
    protected readonly TableData<T> TableData = TableDataHelper<T>.Instance.Build();
    protected string _selectedNs = "";

    protected ResourceCache<T> ItemList = ResourceCacheHelper<T>.Instance.Build();

    [Inject] protected ILogger<TableBase<T>> Logger { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }


    protected async Task OnSearchHandler(string key)
    {
        TableData.OnSearchKeyChanged(key);

        await InvokeAsync(StateHasChanged);
    }


    protected async Task OnNsSelectedHandler(string ns)
    {
        _selectedNs = ns;
        TableData.OnNsSelectedHandler(_selectedNs);
        await InvokeAsync(StateHasChanged);
    }


    protected async Task OnPageChangeHandler(QueryModel<T> queryModel)
    {
        TableData.OnPageChange(queryModel);
        await InvokeAsync(StateHasChanged);
    }


    public void RemoveSelection(string uid)
    {
        TableData.SelectedRows = TableData.SelectedRows.Where(x => x.Metadata.Uid != uid).ToList();
    }
}