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
        private readonly IBaseService _baseService;


        public BlogService(IBaseService baseService)
        {
            _baseService = baseService;
        }


        public async Task<List<Blog>> GetBlogList()
        {
            return await _baseService.GetFromJsonAsync<List<Blog>>("/Blog/GetBlogs");
        }

        public async Task<QueryData<Blog>> Query(QueryPageOptions options)
        {
            return  await _baseService.PostAsJsonAsync<QueryPageOptions,QueryData<Blog>>("/Blog/ListBlogs", options);
        }

        public async Task<bool> Delete(List<int> ids)
        {
            return await _baseService.PostAsJsonAsync<List<int>,bool>("/Blog/DeleteBlog", ids);
        }

        public async Task<bool> Save(Blog oldItem)
        {
             return await _baseService.PostAsJsonAsync<Blog,bool>("/Blog/SaveBlog", oldItem);
        }
    }
}
