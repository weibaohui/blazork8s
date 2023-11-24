using System;
using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Pages.Common;

public partial class ResourceWatcher<T> : ComponentBase where T : IKubernetesObject<V1ObjectMeta>
{
    [Inject]
    protected NavigationManager MyUriHelper { get; set; }

    [Parameter]
    public EventCallback<ResourceCacheHelper<T>> OnResourceChanged { get; set; }

    private ResourceCacheHelper<T> _cache = ResourceCacheHelper<T>.Instance();

    private async Task UpdateMessage(ResourceWatchEntity<T> data)
    {
        Console.WriteLine("page收到消息" + data.Message);
        _cache.Update(data.Type, data.Item);
        await OnResourceChanged.InvokeAsync(_cache);
    }

    protected override async Task OnInitializedAsync()
    {
        var conn = await UIEventHub.Instance().Build(MyUriHelper.ToAbsoluteUri("/chathub"));
        conn.On<ResourceWatchEntity<T>>("ReceiveMessage", UpdateMessage);
    }
}