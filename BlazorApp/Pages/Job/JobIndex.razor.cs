using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Job;
public partial class JobIndex : TableBase<V1Job>
{
    [Inject]
    private IJobService JobService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1Job> data)
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
    private async Task OnItemNameClick(V1Job item)
    {
        await PageDrawerHelper<V1Job>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<JobDetailView, V1Job, bool>(item);
    }
}