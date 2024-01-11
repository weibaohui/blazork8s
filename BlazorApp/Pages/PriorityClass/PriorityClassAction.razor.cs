using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.PriorityClass;
public partial class PriorityClassAction : ComponentBase
{
    [Parameter]
    public V1PriorityClass Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    IMessageService MessageService { get; set; }

    [Inject]
    private IPriorityClassService PriorityClassService { get; set; }
    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
    private async Task OnDeleteClick(V1PriorityClass item)
    {
        await PriorityClassService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }
    private async Task OnYamlClick(V1PriorityClass item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1PriorityClass>, V1PriorityClass, bool>(options, item);
    }
    private async Task OnDocClick(V1PriorityClass item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<V1PriorityClass>, V1PriorityClass, bool>(options, item);
    }

    private async Task OnCancelDefaultClick(V1PriorityClass item)
    {
      await  PriorityClassService.ChangeGlobalDefaultTo(item, false);
      await MessageService.Success($"{item.Name()} Set Global Default to False");
    }
}
