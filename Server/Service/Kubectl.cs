using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using k8s;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Server.Utils;

namespace Server.Service
{
    public class Kubectl
    {
        private readonly ILogger<Kubectl> _logger = ServiceHelper.Services.GetService<ILogger<Kubectl>>();

        private static readonly Lazy<Kubectl> Lazy = new(() => new Kubectl());

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


        public async Task<string> GetResourceJson(string url)
        {
            var s = await Client().HttpClient.GetStringAsync($"{_config.Host}{url}");
            return s;
        }

        public async Task<bool> DeleteResourceJson(string url)
        {
            var s = await Client().HttpClient.DeleteAsync($"{_config.Host}{url}");
            return s.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> PatchResourceJson(string url, string body)
        {
            var content = new StringContent(body);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/strategic-merge-patch+json");
            var s = await Client().HttpClient.PatchAsync($"{_config.Host}{url}", content);
            // _logger.LogInformation(s.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            return s.StatusCode == HttpStatusCode.OK;
        }

        private async Task<T> GetResource<T>(string url)
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


        public async Task<T> GetFromJsonAsync<T>(string url)
        {
            var json = await GetResourceJson(url);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
