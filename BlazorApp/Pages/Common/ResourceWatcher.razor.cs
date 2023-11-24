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
    public EventCallback<ResourceCache<T>> OnResourceChanged { get; set; }

    private ResourceCache<T> _cache = ResourceCacheHelper<T>.Instance.Build();

    private async Task UpdateMessage(ResourceWatchEntity<T> data)
    {
        Console.WriteLine("page收到消息" + data.Message);
        await OnResourceChanged.InvokeAsync(_cache);
    }

    protected override async Task OnInitializedAsync()
    {
        var conn = await UIEventHub.Instance().Build(MyUriHelper.ToAbsoluteUri("/chathub"));
        conn.On<ResourceWatchEntity<T>>("ReceiveMessage", UpdateMessage);
    }
}
