using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;

namespace ui.Service
{
    public class BlogService : IBlogService
    {
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private HttpClient     Http          { get; set; }

         public BlogService(IConfiguration configuration,HttpClient http)
         {
             Configuration = configuration;
             Http          = http;
         }

         
        
        private String GetBaseApiURL()
        {
            var url =Configuration.GetSection("ClientAppSettings").GetValue<String>("BaseApiUrl");
            Console.WriteLine(url + "Inject GetBaseApiURL");
            return url;
        }

        /// <summary>
        /// 后端访问地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private String BE(String url)
        {
            return $"{GetBaseApiURL()}{url}";
        }


        public async Task<List<Blog>> GetBlogList()
        {
            return await Http.GetFromJsonAsync<List<Blog>>(BE("/Blog/GetBlogs"));
        }
    }
}