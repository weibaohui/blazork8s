using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Service
{
    public interface INamespaceAction<T>
    {
        Task<IList<T>> ListItemsByNamespaceAsync(string ns);
    }
}
