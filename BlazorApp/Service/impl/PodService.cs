using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using BlazorApp.Utils;
using k8s;
using k8s.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApp.Service.impl
{
    public class PodService : IPodService
    {
        private readonly IBaseService         _baseService;
        private readonly IServiceScope        _scope;
        private readonly ResourceCache<V1Pod> _cache = ResourceCacheHelper<V1Pod>.Instance.Build();

        public PodService(IBaseService baseService, IServiceScopeFactory serviceScopeFactory)
        {
            _baseService = baseService;
            _scope       = serviceScopeFactory.CreateScope();
            // _watchService = _scope.ServiceProvider.GetService<IWatchService>();
            // Console.WriteLine("PodService 初始化");
        }


        public bool Changed()
        {
            return _cache.Changed();
        }

        public async Task<IList<V1Pod>> ListByOwnerUid(string controllerByUid)
        {
            var list = await ListPods();
            return list.Where(x => x.GetController() != null && x.GetController().Uid == controllerByUid)
                .ToList();
        }


        public async Task<IList<V1Pod>> ListPods()
        {
            if (_cache.Count != 0) return _cache.Get();
            var pods = await _baseService.Client().ListPodForAllNamespacesAsync();
            foreach (var item in pods.Items)
            {
                _cache.Update(WatchEventType.Added, item);
            }

            return _cache.Get();
        }

        private async Task<IList<V1Pod>> ListPodByNamespace(string ns)
        {
            var pods = await ListPods();
            if (string.IsNullOrEmpty(ns))
            {
                return pods;
            }

            return pods.Where(x => x.Namespace() == ns).ToList();
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

        public async Task<IList<V1Pod>> ListItemsByNamespaceAsync(string ns)
        {
            return await ListPodByNamespace(ns);
        }

        public async Task<int> NodePodsNum()
        {
            var pods = await ListPods();
            var tuples = pods.GroupBy(s => s.Spec.NodeName)
                .OrderBy(g => g.Key)
                .Select(g => Tuple.Create(g.Key, g.Count()));
            foreach (var tuple in tuples)
            {
                Console.WriteLine($"{tuple.Item1}={tuple.Item2}");
            }

            return await Task.FromResult(0);
        }
    }
}
