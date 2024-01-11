using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IStorageClassService : ICommonAction<V1StorageClass>
{
    Task SetDefault(V1StorageClass            item);
    Task ChangeGlobalDefaultTo(V1StorageClass item, bool status);
}
