using System;
using k8s;
using k8s.Models;

namespace server.Service.K8s
{
    public class PodWatcher
    {
        public void StartWatch(IKubernetes client)
        {
            var podlistResp = client.ListPodForAllNamespacesWithHttpMessagesAsync(watch: true);
            podlistResp.Watch<V1Pod, V1PodList>((type, item) =>
            {
                Console.WriteLine("==on watch event==");
                Console.WriteLine(type);
                Console.WriteLine(item.Metadata.Name);
                Console.WriteLine("==on watch event==");
            });
        }
    }
}
