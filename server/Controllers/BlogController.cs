using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;
        private IFreeSql fsql;

        public BlogController(ILogger<BlogController> logger, IFreeSql fsql)
        {
            _logger = logger;
            this.fsql = fsql;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<bool>> SaveBlog(Blog blog)
        {
            var rows = await fsql.InsertOrUpdate<Blog>().SetSource(blog).ExecuteAffrowsAsync();
            return Ok(rows > 0);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<bool>> DeleteBlog(List<int> ids)
        {
            var i = await fsql.Update<Blog>()
                .Set(b => b.Url, "http://sample2222.com")
                .Where(b => b.Url == "http://sample.com")
                .ExecuteAffrowsAsync();
            var rows = await fsql.Delete<Blog>().Where(w=>ids.Contains(w.BlogId)).ExecuteAffrowsAsync();
            return Ok(rows > 0);
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Blog> GetBlogs()
        {
            var blogs = fsql.Select<Blog>()
                .Where(b => b.Rating >= 0)
                .OrderByDescending(b => b.BlogId)
                .Page(1, 10)
                .ToList();
            return blogs;
        }

        [HttpPost]
        [Route("[action]")]
        public QueryData<Blog> ListBlogs(QueryPageOptions options)
        {
            _logger.LogInformation($"{options.PageIndex},{options.PageItems}");
            var blogs = fsql.Select<Blog>()
                .Where(b => b.Rating >= 0)
                .OrderByDescending(b => b.BlogId)
                .Count(out var total)
                .Page(options.PageIndex, options.PageItems)
                .ToList();
            return new QueryData<Blog>()
            {
                Items = blogs,
                TotalCount = total,
                IsSorted = true,
                IsFiltered = true,
                IsSearch = true
            };
        }
    }
}