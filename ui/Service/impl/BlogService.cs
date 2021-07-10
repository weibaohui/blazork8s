using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Entity;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

namespace ui.Service.impl
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

         
        
        private string GetBaseApiUrl()
        {
            var url =Configuration.GetSection("ClientAppSettings").GetValue<string>("BaseApiUrl");
            return url;
        }

        /// <summary>
        /// 后端访问地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string B(string url)
        {
            return $"{GetBaseApiUrl()}{url}";
        }


        public async Task<List<Blog>> GetBlogList()
        {
            return await Http.GetFromJsonAsync<List<Blog>>(B("/Blog/GetBlogs"));
        }

        public async Task<QueryData<Blog>> Query(QueryPageOptions options)
        {
            var resp=  await Http.PostAsJsonAsync(B("/Blog/ListBlogs"), options);
           return await resp.Content.ReadFromJsonAsync<QueryData<Blog>>();
        }

        public async Task<bool> Delete(List<int> ids)
        {
            var resp   = await Http.PostAsJsonAsync(B("/Blog/DeleteBlog"), ids);
            var result = await resp.Content.ReadFromJsonAsync<bool>();
            return result;
        }

        public async Task<bool> Save(Blog? oldItem)
        {
            var resp   = await Http.PostAsJsonAsync(B("/Blog/SaveBlog"), oldItem);
            var result = await resp.Content.ReadFromJsonAsync<bool>();
            return result;
        }
    }
}