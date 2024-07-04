using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Utils;
using Entity.Crd.Gateway;

namespace BlazorApp.Pages.Gateway;

public partial class GatewayIndex : TableBase<V1Gateway>
{
    private async Task OnResourceChanged(ResourceCache<V1Gateway> data)
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
}
