using BlazorApp.Chat;
using BlazorApp.Service.k8s;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class ListWatchHelper
{
    private readonly ILogger<ListWatchHelper> _logger = LoggingHelper<ListWatchHelper>.Logger();

    public static  ListWatchHelper  Instance => Nested.Instance;
    private static ListWatchService _watchService;

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly ListWatchHelper Instance = new ListWatchHelper();
    }

    public ListWatchService Create(IKubeService kubeService, IHubContext<ChatHub> ctx)
    {
        _watchService = new ListWatchService(kubeService, ctx);
        return _watchService;
    }

    public void Dispose()
    {
        if (_watchService == null) return;
        _watchService.Dispose();
        _watchService = null;
    }
}
