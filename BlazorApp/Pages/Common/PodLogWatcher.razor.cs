using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Pages.Common;

public partial class PodLogWatcher : ComponentBase
{
    [Inject]
    protected NavigationManager MyUriHelper { get; set; }


    [Parameter]
    public EventCallback<PodLogEntity> OnPodLogChanged { get; set; }


    private async Task UpdatePodLog(PodLogEntity data)
    {
        // Console.WriteLine("page收到日志" + data.LogLineContent);
        await OnPodLogChanged.InvokeAsync(data);
    }

    protected override async Task OnInitializedAsync()
    {
        var conn = await UIEventHub.Instance().Build(MyUriHelper.ToAbsoluteUri("/chathub"));
        conn.On<PodLogEntity>("PodLog", UpdatePodLog);
    }
}
