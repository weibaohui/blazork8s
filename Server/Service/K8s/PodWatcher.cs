using System;
using Entity;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Server.Service.K8s
{
    public class PodWatcher
    {
        private readonly ILogger<PodWatcher>               _logger;
        private readonly IHubContext<ChatHub, IChatClient> _strongChatHubContext;

        public PodWatcher(ILogger<PodWatcher> logger, IHubContext<ChatHub, IChatClient> strongChatHubContext)
        {
            _logger               = logger;
            _strongChatHubContext = strongChatHubContext;
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
            _strongChatHubContext.Clients.All.PodWatch(type,item);
            _logger.LogInformation($"WatchEvent {type}  {item.Metadata.Name}");
        }
    }
}
