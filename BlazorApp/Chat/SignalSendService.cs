using BlazorApp.Service;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApp.Chat;

public class SignalSendService<T> where T : IKubernetesObject<V1ObjectMeta>
{
    private readonly IHubContext<ChatHub> _ctx;

    public SignalSendService(IHubContext<ChatHub> ctx)
    {
        _ctx = ctx;
    }

    public void Send(WatchEventType type, T item)
    {
        _ctx.Clients.All.SendAsync("Resource", (type, item));
    }
}