using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Entity;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using ui.Service;
using Console = System.Console;

namespace ui.Pages
{
    public partial class BlogData : ComponentBase
    {
        [Inject] private IBlogService BlogService { get; set; }

        private        List<Blog>        _blogs;
        private        IEnumerable<Blog> SelectedRows = new List<Blog>();
        private static IEnumerable<int>  PageItemsSource => new int[] {4, 10, 20};

        protected override async Task OnInitializedAsync()
        {
            _blogs = await BlogService.GetBlogList();
        }


        private async Task<QueryData<Blog>> OnQueryAsync(QueryPageOptions options)
        {
            return await BlogService.Query(options);
        }

        private static Task<Blog> OnAddAsync() => Task.FromResult(new Blog() {Name = DateTime.Now.ToShortDateString()});

        private async Task<bool> OnSaveAsync(Blog item)
        {
            var result  = await BlogService.Save(item);
            return await Task.FromResult(result);
        }

        private async Task<bool> OnDeleteAsync(IEnumerable<Blog> items)
        {
            var ids    = items.Select(w => w.BlogId).ToList();
            var result = await BlogService.Delete(ids);
            return await Task.FromResult(result);
        }
    }
}