using System;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace Server.Service.K8s
{
    public class PodWatcher
    {
        private readonly ILogger<PodWatcher> _logger;
        public PodWatcher(ILogger<PodWatcher> logger)
        {
            _logger = logger;
        }

        public void StartWatch(IKubernetes client)
        {
            var podWatcher = client.ListPodForAllNamespacesWithHttpMessagesAsync(watch: true);
            podWatcher.Watch<V1Pod, V1PodList>(EventAction, OnError, OnClosed);
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

        private void EventAction(WatchEventType type, V1Pod item)
        {
            _logger.LogInformation($"WatchEvent {type}  {item.Metadata.Name}");
        }
    }
}
