using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.LimitRange;
public partial class LimitRangeAction : ComponentBase
{
    [Parameter]
    public V1LimitRange Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private ILimitRangeService LimitRangeService { get; set; }

    private async Task OnDeleteClick(V1LimitRange item)
    {
        await LimitRangeService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
