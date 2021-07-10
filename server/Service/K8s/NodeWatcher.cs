using System;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace server.Service.K8s
{
    public class NodeWatcher
    {
        private readonly ILogger<NodeWatcher> _logger;

        public NodeWatcher()
        {
        }

        public NodeWatcher(ILogger<NodeWatcher> logger)
        {
            _logger = logger;
        }

        public async Task StartWatch(IKubernetes client)
        {
            var nodeWatcher = await client.ListNodeWithHttpMessagesAsync(watch: true);
            nodeWatcher.Watch<V1Node, V1NodeList>(EventAction,OnError,OnClosed);
            _logger.LogInformation("Watch started");
        }

        private void OnClosed()
        {
        }

        private void OnError(Exception err)
        {
            _logger.LogError(err.ToString());
        }

        private void EventAction(WatchEventType type, V1Node item)
        {
            Console.WriteLine("==on NodeWatcher watch event==");
            Console.WriteLine(type);
            Console.WriteLine(item.Metadata.Name);
            Console.WriteLine("==on NodeWatcher watch event==");
        }
    }
}
