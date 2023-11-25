using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Service
{
    public interface INamespaceAction<T>
    {
        Task<IList<T>> ListItemsByNamespaceAsync(string ns);
    }
}