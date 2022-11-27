using System.Collections.Generic;
using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service
{
    public interface INodeService
    {
        Task<V1NodeList>                       List();
        Task<V1Node>                           FilterByNodeName(string name);
        Task<(V1Node node, IList<V1Pod> pods)> GetNodeWithPodListByNodeName(string nodeName);

    }
}
