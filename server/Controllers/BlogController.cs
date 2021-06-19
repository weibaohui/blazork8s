using System.Collections.Generic;
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
        public Blog SaveBlog(Blog blog)
        {
            blog.BlogId = (int) fsql.Insert<Blog>()
                .AppendData(blog)
                .ExecuteIdentity();
            return blog;
            
        }
        [HttpPost]
        [Route("[action]")]
        public int UpdateBlog()
        {
            var i = fsql.Update<Blog>()
                .Set(b => b.Url, "http://sample2222.com")
                .Where(b => b.Url == "http://sample.com")
                .ExecuteAffrows();
            return i;
        }
        [HttpGet]
        public List<Blog> GetBlogs()
        {
            var blogs = fsql.Select<Blog>()
                .Where(b => b.Rating >= 0)
                .OrderByDescending(b => b.BlogId)
                .Count(out var total)
                .Page(1, 3)
                .ToList();
            _logger.LogError(total.ToString());
            return blogs;
        }
    }
}