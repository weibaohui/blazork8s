using System.Threading.Tasks;
using BlazorApp.Pages.Ai;
using BlazorApp.Pages.Common;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.CronJob;

public partial class CronJobIndex : TableBase<V1CronJob>
{
    [Inject] private IPromptService PromptService { get; set; }
    [Inject] private ICronJobService CronJobService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1CronJob> data)
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

    private async Task OnItemNameClick(V1CronJob item)
    {
        await PageDrawerHelper<V1CronJob>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<CronJobDetailView, V1CronJob, bool>(item);
    }

    private async Task OnAnyChatClick(string item)
    {
        var options = PageDrawerService.DefaultOptions($"{L["AI Chat"]}", 1000);
        await PageDrawerService.ShowDrawerAsync<AnyChat, string, bool>(options, item);
    }

    private async Task OnNextCronDateClick(string item)
    {
        var options = PageDrawerService.DefaultOptions($"{L["Next"]}", 500);
        await PageDrawerService.ShowDrawerAsync<NextCronDateView, string, bool>(options, item);
    }
}
