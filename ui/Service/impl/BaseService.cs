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
        [Inject]
        private HttpClient Http { get; set; }

        [Inject]
        private IConfiguration Configuration { get; set; }

        public BaseService(HttpClient http, IConfiguration configuration)
        {
            Http          = http;
            Configuration = configuration;
        }


        private string GetBaseApiUrl()
        {
            var url = Configuration.GetSection("ClientAppSettings").GetValue<string>("BaseApiUrl");
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
            return await Http.GetFromJsonAsync<T>(Url(url));
        }

        public async Task<R> PostAsJsonAsync<T, R>(string url, T t)
        {
            var resp   = await Http.PostAsJsonAsync(Url(url), t);
            var result = await resp.Content.ReadFromJsonAsync<R>();
            return result;
        }
    }
}
