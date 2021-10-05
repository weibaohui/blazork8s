using Entity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Server.Service.K8s
{
    public class Watcher:IWatcher
    {
        private readonly ILogger<Watcher>                  _logger;
        private readonly PodWatcher                        _podWatcher;
        private readonly NodeWatcher                       _nodeWatcher;
        private readonly IHubContext<ChatHub, IChatClient> _strongChatHubContext;

        public Watcher(ILogger<Watcher> logger, PodWatcher podWatcher, NodeWatcher nodeWatcher, IHubContext<ChatHub, IChatClient> strongChatHubContext)
        {
            _logger               = logger;
            _podWatcher           = podWatcher;
            _nodeWatcher          = nodeWatcher;
            _strongChatHubContext = strongChatHubContext;
        }

        public void StartWatch()
        {
            var cli = Kubectl.Instance.Client();
            _nodeWatcher.StartWatch(cli);
            _podWatcher.StartWatch(cli);
        }
    }

    public interface IWatcher
    {
        void StartWatch();
    }
}
