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

    protected ResourceCacheHelper<T> ItemList = ResourceCacheHelper<T>.Instance();

    protected readonly        TableDataHelper<T>    TableDataHelper = new();
    protected static readonly ILogger<TableBase<T>> Logger          = LoggingHelper<TableBase<T>>.Logger();
    private                   string                _selectedNs     = "";


    protected async Task OnSearchHandler(string key)
    {
        await TableDataHelper.SearchName(key);

        await InvokeAsync(StateHasChanged);
    }


    protected async Task OnNsSelectedHandler(string ns)
    {
        _selectedNs = ns;
        await TableDataHelper.OnNsSelectedHandler(_selectedNs);
        await InvokeAsync(StateHasChanged);
    }


    protected async Task OnPageChangeHandler(QueryModel<T> queryModel)
    {
        await TableDataHelper.OnChange(queryModel);
        await InvokeAsync(StateHasChanged);
    }


    public void RemoveSelection(string uid)
    {
        TableDataHelper.SelectedRows = TableDataHelper.SelectedRows.Where(x => x.Metadata.Uid != uid);
    }
}
