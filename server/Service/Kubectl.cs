using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Entity;
using k8s;
using k8s.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using server.Utils;

namespace server.Service
{
    public class Kubectl
    {
        private readonly ILogger<Kubectl> _logger = ServiceHelper.Services.GetService<ILogger<Kubectl>>();

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
            var url = "/api/v1/nodes/docker-desktop";
            // var json = GetResourceJson(url);
            // Console.WriteLine($"xxxxxxx---{json.Result}");
            var nodeList = await GetResource<JsonNode>(url);
            var capacity = nodeList.Status.Capacity;
            foreach (var kv in capacity)
            {
                Console.WriteLine($"{kv.Key}-{kv.Value}");
            }
        }

        public async Task<string> GetResourceJson(string url)
        {
            var s = await Client().HttpClient.GetStringAsync($"{_config.Host}{url}");
            return s;
        }

        public async Task<T> GetResource<T>(string url)
        {
            try
            {
                var t = await Client().HttpClient.GetFromJsonAsync<T>($"{_config.Host}{url}");
                return t;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
