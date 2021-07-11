using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace server.Service
{
    public class Kubectl
    {
        private static readonly Lazy<Kubectl> Lazy = new Lazy<Kubectl>(() => new Kubectl());

        public static Kubectl Instance => Lazy.Value;

        private IKubernetes _client = null;

        public IKubernetes Client()
        {
            if (_client == null)
            {
                var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();

                _client = new Kubernetes(config);
            }
            return _client;
        }

        public async Task getNodes()
        {
            Console.WriteLine("getNodes");
            try
            {
                var y=  await Client().HttpClient.GetStringAsync("https://kubernetes.docker.internal:6443/api/v1/nodes/docker-desktop");
                // var x= await  Client().HttpClient.GetFromJsonAsync<V1NodeList>("https://kubernetes.docker.internal:6443/api/v1/nodes");
                Console.WriteLine($"xxxxxxx---{y}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

}
