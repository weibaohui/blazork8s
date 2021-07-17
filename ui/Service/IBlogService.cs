using System.Collections.Generic;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Entity;

namespace ui.Service
{
    public interface IBlogService
    {
        Task<List<Blog>>      GetBlogList();
        Task<QueryData<Blog>> Query(QueryPageOptions options);
        Task<bool>            Delete(List<int>       ids);
        Task<bool>            Save(Blog             oldItem);
    }
}