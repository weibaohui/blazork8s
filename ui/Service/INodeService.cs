using System.Threading.Tasks;
using Entity;
using k8s.Models;

namespace ui.Service
{
    public interface INodeService
    {
        Task<V1NodeList> List();
    }
}
