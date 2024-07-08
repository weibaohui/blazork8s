using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd.Gateway;

public class V1Alpha2UDPRouteList : IKubernetesObject<V1ListMeta>, IItems<V1Alpha2UDPRoute>
{
    [JsonPropertyName("items")] public IList<V1Alpha2UDPRoute> Items { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ListMeta Metadata { get; set; }
}

[KubernetesEntity(Group = KubeGroup, Kind = KubeKind, ApiVersion = KubeApiVersion, PluralName = KubePluralName)]
public class V1Alpha2UDPRoute : IKubernetesObject<V1ObjectMeta>, ISpec<UDPRouteSpec>
{
    public const string KubeApiVersion = "v1alpha2";
    public const string KubeKind = "UDPRoute";
    public const string KubeGroup = "gateway.networking.k8s.io";
    public const string KubePluralName = "udproutes";
    [JsonPropertyName("status")] public UDPRouteStatus Status { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ObjectMeta Metadata { get; set; }

    [JsonPropertyName("spec")] public UDPRouteSpec Spec { get; set; }
}

public class UDPRouteSpec
{
    [JsonPropertyName("parentRefs")] public IList<ParentReference> ParentRefs { get; set; }
    [JsonPropertyName("rules")] public IList<UDPRouteRule> Rules { get; set; }
}

public class UDPRouteRule
{
    [JsonPropertyName("backendRefs")] public List<UDPBackendRef> BackendRefs { get; set; }
}

public class UDPBackendRef
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("group")] public string Group { get; set; }

    [JsonPropertyName("namespace")] public string Namespace { get; set; }

    [JsonPropertyName("port")] public int Port { get; set; }

    [JsonPropertyName("weight")] public int Weight { get; set; }
}

public class UDPRouteStatus
{
    [JsonPropertyName("parents")] public IList<RouteParentStatus> Parents { get; set; }
}
