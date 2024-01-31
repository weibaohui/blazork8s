using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface INodeService : ICommonAction<V1Node>
{
    long   GetPodCount(string         nodeName);
    string GetPodCapacityCount(string nodeName);
}
