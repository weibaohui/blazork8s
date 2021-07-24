using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

namespace ui.Service.impl
{
    public class BaseService : IBaseService
    {

        private readonly HttpClient _http;

        private readonly IConfiguration _configuration;

        public BaseService(HttpClient http, IConfiguration configuration)
        {
            _http          = http;
            _configuration = configuration;
        }


        private string GetBaseApiUrl()
        {
            var url = _configuration.GetSection("ClientAppSettings").GetValue<string>("BaseApiUrl");
            return url;
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

        public async Task<T> GetFromJsonAsync<T>(string url)
        {
            return await _http.GetFromJsonAsync<T>(Url(url));
        }

        public async Task<R> PostAsJsonAsync<T, R>(string url, T t)
        {
            var resp   = await _http.PostAsJsonAsync(Url(url), t);
            var result = await resp.Content.ReadFromJsonAsync<R>();
            return result;
        }
    }
}
