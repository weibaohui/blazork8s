using System.Threading.Tasks;
using Entity;

namespace ui.Service
{
    public interface INodeService
    {
        Task<JsonNodeList> List();
    }
}
