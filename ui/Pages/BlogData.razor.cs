using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Entity;
using Microsoft.AspNetCore.Components;

namespace ui.Pages
{
    public partial class BlogData : ComponentBase
    {
        [Inject] private HttpClient Http { get; set; }

        List<Blog> blogs;
        private IEnumerable<Blog> SelectedRows = new List<Blog>();
        private static IEnumerable<int> PageItemsSource => new int[] {4, 10, 20};

        protected override async Task OnInitializedAsync()
        {
            blogs = await Http.GetFromJsonAsync<List<Blog>>("https://localhost:4001/Blog/GetBlogs");
        }

        private async Task<QueryData<Blog>> OnQueryAsync(QueryPageOptions options)
        {
            var resp = await Http.PostAsJsonAsync("https://localhost:4001/Blog/ListBlogs", options);
            return await resp.Content.ReadFromJsonAsync<QueryData<Blog>>();
        }

        private static Task<Blog> OnAddAsync() => Task.FromResult(new Blog() {Name = DateTime.Now.ToShortDateString()});

        private async Task<bool> OnSaveAsync(Blog item)
        {
            
                var oldItem = blogs.FirstOrDefault(i => i.BlogId == item.BlogId) ?? new Blog();
                oldItem = item;
                var resp = await Http.PostAsJsonAsync("https://localhost:4001/Blog/SaveBlog", oldItem);
                var result = await resp.Content.ReadFromJsonAsync<bool>();
                return await Task.FromResult(result);
             
        }

        private async Task<bool> OnDeleteAsync(IEnumerable<Blog> items)
        {
            var ids = items.Select(w=>w.BlogId).ToList();
            var resp = await Http.PostAsJsonAsync("https://localhost:4001/Blog/DeleteBlog", ids);
            var result = await resp.Content.ReadFromJsonAsync<bool>();
            return await Task.FromResult(result);
          
        }
    }
}