using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface INodeService : ICommonAction<V1Node>
{
    long   GetPodCount(string         nodeName);
    string GetPodCapacity(string nodeName);
    string GetMemoryCapacity(string   nodeName);
    string GetCpuCapacity(string      nodeName);
}
