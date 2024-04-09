using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Pages.Common;

public partial class ResourceWatcher<T> : PageBase where T : IKubernetesObject<V1ObjectMeta>
{
    private ResourceCache<T> _cache = ResourceCacheHelper<T>.Instance.Build();

    private HubConnection HubConnection { get; set; }

    [Inject]
    protected NavigationManager MyUriHelper { get; set; }

    [Parameter]
    public EventCallback<ResourceCache<T>> OnResourceChanged { get; set; }

    [Parameter]
    public EventCallback<PodLogEntity> OnPodLogChanged { get; set; }

    private async Task UpdateMessage(ResourceWatchEntity<T> data)
    {
        await OnResourceChanged.InvokeAsync(_cache);
    }

    private async Task UpdatePodLog(PodLogEntity data)
    {
        // Console.WriteLine("page收到日志" + data.LogLineContent);
        await OnPodLogChanged.InvokeAsync(data);
    }

    protected override Task OnInitializedAsync()
    {
        HubConnection = new HubConnectionBuilder()
            .WithUrl(MyUriHelper.ToAbsoluteUri("/chathub"))
            .Build();
        return Task.CompletedTask;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            HubConnection.On<ResourceWatchEntity<T>>("ReceiveMessage", UpdateMessage);
            await HubConnection.StartAsync();
        }
    }
}
