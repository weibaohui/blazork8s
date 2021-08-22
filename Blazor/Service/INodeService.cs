using System.Threading.Tasks;
using k8s.Models;

namespace Blazor.Service
{
    public interface INodeService
    {
        Task<V1NodeList> List();
    }
}