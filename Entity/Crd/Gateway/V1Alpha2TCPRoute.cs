using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd.Gateway;

public class V1Alpha2TCPRouteList : IKubernetesObject<V1ListMeta>, IItems<V1Alpha2TCPRoute>
{
    [JsonPropertyName("items")] public IList<V1Alpha2TCPRoute> Items { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ListMeta Metadata { get; set; }
}

[KubernetesEntity(Group = KubeGroup, Kind = KubeKind, ApiVersion = KubeApiVersion, PluralName = KubePluralName)]
public class V1Alpha2TCPRoute : IKubernetesObject<V1ObjectMeta>, ISpec<TCPRouteSpec>
{
    public const string KubeApiVersion = "v1alpha2";
    public const string KubeKind = "TCPRoute";
    public const string KubeGroup = "gateway.networking.k8s.io";
    public const string KubePluralName = "tcproutes";
    [JsonPropertyName("status")] public TCPRouteStatus Status { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ObjectMeta Metadata { get; set; }

    [JsonPropertyName("spec")] public TCPRouteSpec Spec { get; set; }
}

public class TCPRouteSpec
{
    [JsonPropertyName("parentRefs")] public IList<ParentReference> ParentRefs { get; set; }
    [JsonPropertyName("rules")] public IList<TCPRouteRule> Rules { get; set; }
}

public class TCPRouteRule
{
    [JsonPropertyName("backendRefs")] public List<TCPBackendRef> BackendRefs { get; set; }
}

public class TCPBackendRef
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("group")] public string Group { get; set; }

    [JsonPropertyName("namespace")] public string Namespace { get; set; }

    [JsonPropertyName("port")] public int Port { get; set; }

    [JsonPropertyName("weight")] public int Weight { get; set; }
}

public class TCPRouteStatus
{
    [JsonPropertyName("parents")] public IList<RouteParentStatus> Parents { get; set; }
}
