using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;

namespace ui.Service
{
    public interface IBlogService
    {
        Task<List<Blog>> GetBlogList();
    }
}