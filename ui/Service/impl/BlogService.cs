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
        [Inject]
        private IBaseService BaseService { get; set; }
        [Inject]
        private HttpClient Http { get; set; }


        public BlogService(IBaseService baseService, HttpClient http)
        {
            BaseService = baseService;
            Http        = http;
        }


        public async Task<List<Blog>> GetBlogList()
        {
            return await BaseService.GetFromJsonAsync<List<Blog>>("/Blog/GetBlogs");
        }

        public async Task<QueryData<Blog>> Query(QueryPageOptions options)
        {
            return  await BaseService.PostAsJsonAsync<QueryPageOptions,QueryData<Blog>>("/Blog/ListBlogs", options);
        }

        public async Task<bool> Delete(List<int> ids)
        {
            return await BaseService.PostAsJsonAsync<List<int>,bool>("/Blog/DeleteBlog", ids);
        }

        public async Task<bool> Save(Blog oldItem)
        {
             return await BaseService.PostAsJsonAsync<Blog,bool>("/Blog/SaveBlog", oldItem);
        }
    }
}
