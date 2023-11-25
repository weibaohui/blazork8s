using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.impl
{
    public class PodService : CommonAction<V1Pod>, IPodService
    {
        private readonly IBaseService _baseService;

        public PodService(IBaseService baseService)
        {
            _baseService = baseService;
        }


        public IList<V1Pod> ListByNodeName(string nodeName)
        {
            var list = List();
            return list.Where(x => x.Spec.NodeName == nodeName)
                .ToList();
        }


        //
        public async Task<bool> DeletePod(string ns, string name)
        {
            // Console.WriteLine($"DeletePod,{ns},{name}");
            return await _baseService.Client().DeleteNamespacedPodAsync(name, ns) != null;
        }

        public async Task<WebSocket> ExecInPod(V1Pod pod, string command)
        {
            // var x = await _baseService.Client().ConnectPostNamespacedPodExecAsync(pod.Metadata.Name,
            //     pod.Metadata.NamespaceProperty,
            //     container: pod.Spec.Containers[0].Name,
            //     command: command
            // );
            // return x;

            var webSocket =
                await _baseService.Client()
                    .WebSocketNamespacedPodExecAsync(pod.Metadata.Name,
                        pod.Namespace(),
                        command,
                        pod.Spec.Containers[0].Name).ConfigureAwait(false);

            // var demux = new StreamDemuxer(webSocket);
            return webSocket;
        }

        public async Task<Stream> Logs(V1Pod pod, bool follow = false, bool previous = false)
        {
            var response = await _baseService.Client().CoreV1.ReadNamespacedPodLogWithHttpMessagesAsync(
                pod.Metadata.Name,
                pod.Metadata.NamespaceProperty,
                container: pod.Spec.Containers[0].Name,
                tailLines: 1,
                previous: previous,
                follow: follow).ConfigureAwait(false);
            var stream = response.Body;
            return stream;
            // await stream.CopyToAsync(Console.OpenStandardOutput());
        }

        public async Task<Stream> Logs(string podNs, string podName, string containerName, bool follow = false,
            bool                              previous = false)
        {
            var response = await _baseService.Client().CoreV1.ReadNamespacedPodLogWithHttpMessagesAsync(
                podName,
                podNs,
                container: containerName,
                previous: previous,
                follow: follow).ConfigureAwait(false);
            var stream = response.Body;
            return stream;
            // await stream.CopyToAsync(Console.OpenStandardOutput());
        }


        public IEnumerable<Tuple<string, int>> NodePodsNum()
        {
            var pods = List();
            var tuples = pods.GroupBy(s => s.Spec.NodeName)
                .OrderBy(g => g.Key)
                .Select(g => Tuple.Create(g.Key, g.Count()));
            foreach (var tuple in tuples)
            {
                Console.WriteLine($"{tuple.Item1}={tuple.Item2}");
            }

            return tuples;
        }
    }
}
