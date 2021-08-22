using System.Threading.Tasks;
using k8s.Models;
using WebApiClientCore.Attributes;

namespace Blazor.Service
{
    [HttpHost("https://localhost:4001/")]
    [JsonNetReturn]
    public interface INodeApi
    {
        [HttpGet("/KubeApi/api/v1/nodes/")]
        Task<V1NodeList> List();
    }
}