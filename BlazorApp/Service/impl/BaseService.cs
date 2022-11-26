using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using k8s;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BlazorApp.Service.impl
{
    public class BaseService : IBaseService
    {
        private readonly HttpClient _http;

        private readonly IConfiguration _configuration;

        private readonly IKubeService _kubeService;
        public BaseService(HttpClient http, IConfiguration configuration,IKubeService kubeService)
        {
            _http          = http;
            _configuration = configuration;
            _kubeService   = kubeService;
        }


        private string GetBaseApiUrl()
        {
            var url = _configuration.GetSection("ClientAppSettings").GetValue<string>("BaseApiUrl");
            return url;
        }

        public Kubernetes Client()
        {
            return _kubeService.Client();
        }

        /// <summary>
        /// 后端访问地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string Url(string url)
        {
            return $"{GetBaseApiUrl()}{url}";
        }

        public async Task<T> GetFromJsonAsyncN<T>(string url)
        {
            return await _http.GetFromJsonAsync<T>(Url(url));
        }

        public async Task<R> PostAsJsonAsync<T, R>(string url, T t)
        {
            var resp   = await _http.PostAsJsonAsync(Url(url), t);
            var result = await resp.Content.ReadFromJsonAsync<R>();
            return result;
        }

        public async Task<string> GetStringAsync(string url)
        {
            return await _http.GetStringAsync(Url(url));
        }

        private async Task<HttpResponseMessage> DeleteStringAsync(string url)
        {
            return await _http.DeleteAsync(Url(url));
        }

        public async Task<T> GetFromJsonAsync<T>(string url)
        {
            var json = await GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<bool> DeleteAsync<T>(string url)
        {
            var msg = await DeleteStringAsync(url);
            return "true" == msg.Content.ToString();
        }
    }
}
