using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using BlazorApp.Service.k8s;
using BlazorApp.Utils.Prometheus.Models.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Cluster;

public partial class Metrics : ComponentBase
{
    private bool  _apiServerRequestDetailVisible        = false;
    private bool  _apiServerRequestLatencyDetailVisible = false;
    private bool  _apiServerRequestSliDetailVisible     = false;
    private bool  _apiServerResponseSizeDetailVisible   = false;
    private bool  _etcdRequestLatencyDetailVisible      = false;
    private bool  _etcdStorageDetailVisible             = false;
    private Timer _timer;

    [Inject]
    public IKubeService KubeService { get; set; }

    private IList<IMetric> AllMetrics { get; set; }


    protected override async Task OnInitializedAsync()
    {
        _timer         =  new Timer(10000);
        _timer.Elapsed += async (sender, eventArgs) => await OnTimerCallback();
        _timer.Start();
        await OnTimerCallback(); //先执行一次
        await base.OnInitializedAsync();
    }

    private async Task OnTimerCallback()
    {
        AllMetrics = await KubeService.GetMetrics();
        await InvokeAsync(StateHasChanged);
    }
}
