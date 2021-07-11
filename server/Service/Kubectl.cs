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

        private IKubernetes                   _client = null;
        private KubernetesClientConfiguration _config = null;

        public IKubernetes Client()
        {
            if (_client == null)
            {
                _config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
                _client = new Kubernetes(_config);
            }

            return _client;
        }

        public async Task getNodes()
        {
            var url          = "/api/v1/nodes";
            var json = GetResourceJson(url);
            Console.WriteLine($"xxxxxxx---{json.Result}");
        }

        public async Task<string> GetResourceJson(string url)
        {
            var s = await Client().HttpClient.GetStringAsync($"{_config.Host}{url}");
            return s;
        }
    }
}
