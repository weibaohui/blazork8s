using Microsoft.Extensions.Logging;

namespace Server.Service.K8s
{
    public class Watcher
    {
        private readonly ILogger<Watcher> _logger;
        private readonly PodWatcher       _podWatcher;
        private readonly NodeWatcher      _nodeWatcher;

        public Watcher(PodWatcher podWatcher, NodeWatcher nodeWatcher, ILogger<Watcher> logger)
        {
            _podWatcher  = podWatcher;
            _nodeWatcher = nodeWatcher;
            _logger      = logger;
            logger.LogInformation("Watcher 实例化");
        }

        public void StartWatch()
        {
            var cli = Kubectl.Instance.Client();
            _nodeWatcher.StartWatch(cli);
            _podWatcher.StartWatch(cli);
        }
    }
}
