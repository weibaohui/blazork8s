using System;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace server.Service.K8s
{
    public class NodeWatcher
    {
        public async Task StartWatch(IKubernetes client)
        {
            var nodeWatcher = await client.ListNodeWithHttpMessagesAsync(watch: true);
            nodeWatcher.Watch<V1Node, V1NodeList>((type, item) =>
            {
                Console.WriteLine("==on NodeWatcher watch event==");
                Console.WriteLine(type);
                Console.WriteLine(item.Metadata.Name);
                Console.WriteLine("==on NodeWatcher watch event==");
            });
        }
    }
}
