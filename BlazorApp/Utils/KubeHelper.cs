using k8s.Models;

namespace BlazorApp.Utils;

public static class KubeHelper
{
    /// <summary>
    ///     创建一个资源的引用对象
    /// </summary>
    /// <param name="kind"></param>
    /// <param name="ns"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static V1ObjectReference GetObjectRef(string kind, string ns, string name)
    {
        var reference = new V1ObjectReference
        {
            ApiVersion = "v1",
            Kind = kind,
            Name = name,
            NamespaceProperty = ns
        };
        return reference;
    }

    public static V1ObjectReference GetObjectRef(string kind, string name)
    {
        var reference = new V1ObjectReference
        {
            ApiVersion = "v1",
            Kind = kind,
            Name = name
        };
        return reference;
    }

    public static V1ObjectReference GetObjectRef(string apiVersion, string kind, string ns, string name)
    {
        var reference = new V1ObjectReference
        {
            ApiVersion = apiVersion,
            Kind = kind,
            Name = name,
            NamespaceProperty = ns
        };
        return reference;
    }
}
