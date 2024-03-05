using System.Threading.Tasks;
using BlazorApp.Pages.ai;
using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.CronJob;

public partial class CronJobDetailView :  DrawerPageBase<V1CronJob>
{
    private V1CronJob CronJob { get; set; }
    protected override async Task OnInitializedAsync()
    {
        CronJob = base.Options;
        await base.OnInitializedAsync();
    }


    private async Task OnAnyChatClick(string item)
    {
        var options = PageDrawerService.DefaultOptions($"AI Chat", width: 1000);
        await PageDrawerService.ShowDrawerAsync<AnyChat, string, bool>(options, item);
    }
    private async Task OnNextCronDateClick(string item)
    {
        var options = PageDrawerService.DefaultOptions($"Next Cron Date", width: 500);
        await PageDrawerService.ShowDrawerAsync<NextCronDateView, string, bool>(options, item);
    }
}
