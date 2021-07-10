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

        public NodeWatcher(ILogger<NodeWatcher> logger)
        {
            _logger = logger;
        }

        public void StartWatch(IKubernetes client)
        {
            var nodeWatcher = client.ListNodeWithHttpMessagesAsync(watch: true);
            nodeWatcher.Watch<V1Node, V1NodeList>(EventAction, OnError, OnClosed);
            _logger.LogInformation("Watch started");
        }

        private void OnClosed()
        {
            _logger.LogError("监听结束");
        }

        private void OnError(Exception err)
        {
            _logger.LogError(err.ToString());
        }

        private void EventAction(WatchEventType type, V1Node item)
        {
            _logger.LogInformation($"WatchEvent {type}  {item.Metadata.Name}");

            switch (type)
            {
                case WatchEventType.Added:
                    NodeService.Instance.AddNode(item);
                    break;
                case WatchEventType.Modified:
                    NodeService.Instance.ModifyNode(item);
                    break;
                case WatchEventType.Deleted:
                    NodeService.Instance.RemoveNode(item);
                    break;
                case WatchEventType.Error:
                    break;
                case WatchEventType.Bookmark:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
