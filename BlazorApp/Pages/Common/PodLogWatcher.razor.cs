using System;
using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Pages.Common;

public partial class PodLogWatcher : PageBase, IDisposable
{
    [Inject] protected NavigationManager MyUriHelper { get; set; }


    [Parameter] public EventCallback<PodLogEntity> OnPodLogChanged { get; set; }

    [Parameter] public V1Pod PodItem { get; set; }

    [Parameter] public string ContainerName { get; set; }

    public void Dispose()
    {
        PodLogExecutorHelper.Instance.GetOrCreate(PodItem.Namespace(), PodItem.Name(), ContainerName).Kill();
    }


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