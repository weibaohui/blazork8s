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

        Blog[] blogs;
        private IEnumerable<Blog> SelectedRows = new List<Blog>();
        private static IEnumerable<int> PageItemsSource => new int[] {4, 10, 20};

        protected override async Task OnInitializedAsync()
        {
            blogs = await Http.GetFromJsonAsync<Blog[]>("https://localhost:4001/Blog/GetBlogs");
        }

        private async Task<QueryData<Blog>> OnQueryAsync(QueryPageOptions options)
        {
            var resp = await Http.PostAsJsonAsync("https://localhost:4001/Blog/ListBlogs", options);
            return await resp.Content.ReadFromJsonAsync<QueryData<Blog>>();
        }
    }
}