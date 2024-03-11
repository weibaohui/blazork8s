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
    private ResourceCache<T> _cache = ResourceCacheHelper<T>.Instance.Build();

    [Inject]
    protected NavigationManager MyUriHelper { get; set; }

    [Parameter]
    public EventCallback<ResourceCache<T>> OnResourceChanged { get; set; }

    [Parameter]
    public EventCallback<PodLogEntity> OnPodLogChanged { get; set; }

    private async Task UpdateMessage(ResourceWatchEntity<T> data)
    {
        Console.WriteLine("page收到消息" + data.Message);
        await OnResourceChanged.InvokeAsync(_cache);
    }

    private async Task UpdatePodLog(PodLogEntity data)
    {
        // Console.WriteLine("page收到日志" + data.LogLineContent);
        await OnPodLogChanged.InvokeAsync(data);
    }

    protected override async Task OnInitializedAsync()
    {
        var conn = await UIEventHub.Instance().Build(MyUriHelper.ToAbsoluteUri("/chathub"));
        //todo 一种类型维持一个链接，否则只有一个页面能进行页面响应，其他的不响应。bug
        conn.On<ResourceWatchEntity<T>>("ReceiveMessage", UpdateMessage);
        conn.On<PodLogEntity>("PodLog", UpdatePodLog);
    }
}
