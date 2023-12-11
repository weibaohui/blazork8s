using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Utils;
using Entity;

namespace BlazorApp.Pages.PortForwarding;

public partial class PortForwardIndex : TableBase<PortForward>
{



    private async Task OnResourceChanged(ResourceCache<PortForward> data)
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


    private async Task OnItemNameClick(PortForward item)
    {


    }
}
