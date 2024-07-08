using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd.Gateway;

public class V1Alpha2TLSRouteList : IKubernetesObject<V1ListMeta>, IItems<V1Alpha2TLSRoute>
{
    [JsonPropertyName("items")] public IList<V1Alpha2TLSRoute> Items { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ListMeta Metadata { get; set; }
}

[KubernetesEntity(Group = KubeGroup, Kind = KubeKind, ApiVersion = KubeApiVersion, PluralName = KubePluralName)]
public class V1Alpha2TLSRoute : IKubernetesObject<V1ObjectMeta>, ISpec<TLSRouteSpec>
{
    public const string KubeApiVersion = "v1alpha2";
    public const string KubeKind = "TLSRoute";
    public const string KubeGroup = "gateway.networking.k8s.io";
    public const string KubePluralName = "tlsroutes";
    [JsonPropertyName("status")] public TLSRouteStatus Status { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ObjectMeta Metadata { get; set; }

    [JsonPropertyName("spec")] public TLSRouteSpec Spec { get; set; }
}

public class TLSRouteSpec
{
    [JsonPropertyName("parentRefs")] public IList<ParentReference> ParentRefs { get; set; }
    [JsonPropertyName("rules")] public IList<TLSRouteRule> Rules { get; set; }
    [JsonPropertyName("hostnames")] public IList<string> Hostnames { get; set; }
}

public class TLSRouteRule
{
    [JsonPropertyName("backendRefs")] public List<TLSBackendRef> BackendRefs { get; set; }
}

public class TLSBackendRef
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("group")] public string Group { get; set; }

    [JsonPropertyName("namespace")] public string Namespace { get; set; }

    [JsonPropertyName("port")] public int Port { get; set; }

    [JsonPropertyName("weight")] public int Weight { get; set; }
}

public class TLSRouteStatus
{
    [JsonPropertyName("parents")] public IList<RouteParentStatus> Parents { get; set; }
}
