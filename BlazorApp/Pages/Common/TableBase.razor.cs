using System.Linq;
using System.Threading.Tasks;
using AntDesign.TableModels;
using BlazorApp.Service;
using BlazorApp.Utils;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.Common;

public partial class TableBase<T> : ComponentBase where T : IKubernetesObject<V1ObjectMeta>
{
    [Inject]
    protected IPageDrawerService PageDrawerService { get; set; }

    protected ResourceCache<T> ItemList = ResourceCacheHelper<T>.Instance.Build();

    protected readonly        TableData<T>          TableData   = TableDataHelper<T>.Instance.Build();
    protected static readonly ILogger<TableBase<T>> Logger      = LoggingHelper<TableBase<T>>.Logger();
    private                   string                _selectedNs = "";


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
        TableData.SelectedRows = TableData.SelectedRows.Where(x => x.Metadata.Uid != uid);
    }
}
