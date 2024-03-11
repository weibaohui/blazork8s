using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IResourceCrudService
{
    Task<object> DeleteItem(string itemKind, string ns, string name);
}