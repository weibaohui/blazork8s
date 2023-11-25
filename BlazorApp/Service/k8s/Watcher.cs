using System.Threading.Tasks;
using BlazorApp.Chat;
using BlazorApp.Utils;
using Entity;
using k8s;
using k8s.Autorest;
using k8s.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.k8s;

public class Watcher<T, TL> where T : IKubernetesObject<V1ObjectMeta>
{
    private readonly        IHubContext<ChatHub>    _ctx;
    private static readonly ILogger<Watcher<T, TL>> Logger = LoggingHelper<Watcher<T, TL>>.Logger();

    public Watcher(IHubContext<ChatHub> ctx)
    {
        _ctx = ctx;
    }

    public async Task Watch(Task<HttpOperationResponse<TL>> listResp)
    {
        var cache = ResourceCacheHelper<T>.Instance.Build();
        await foreach (var (type, item) in listResp.WatchAsync<T, TL>())
        {
            var data = new ResourceWatchEntity<T>
            {
                Message = $"{type}:{item.Kind}:{item.Metadata.Name}",
                Type    = type,
                Item    = item
            };
            Logger.LogInformation($"{type}:{item.Kind}:{item.Metadata.Name}");
            cache.Update(type, item);
            await _ctx.Clients.All.SendAsync("ReceiveMessage", data);
        }
    }
}
