using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.StatefulSet;

public partial class StatefulSetAction : ComponentBase
{
    [Parameter]
    public V1StatefulSet Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

    [Inject]
    private IStatefulSetService StatefulSetService { get; set; }


    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    [Inject]
    private ILogger<StatefulSetAction> Logger { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task OnStatefulSetDeleteClick(V1StatefulSet item)
    {
        await StatefulSetService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

 

    private async Task OnYamlClick(V1StatefulSet item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1StatefulSet>, V1StatefulSet, bool>(options, item);
    }
}
