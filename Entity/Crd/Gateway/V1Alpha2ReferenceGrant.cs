using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd.Gateway;

public class V1Alpha2ReferenceGrantList : IKubernetesObject<V1ListMeta>, IItems<V1Alpha2ReferenceGrant>
{
    [JsonPropertyName("items")] public IList<V1Alpha2ReferenceGrant> Items { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ListMeta Metadata { get; set; }
}

[KubernetesEntity(Group = KubeGroup, Kind = KubeKind, ApiVersion = KubeApiVersion, PluralName = KubePluralName)]
public class V1Alpha2ReferenceGrant : IKubernetesObject<V1ObjectMeta>, ISpec<ReferenceGrantSpec>
{
    public const string KubeApiVersion = "v1alpha2";
    public const string KubeKind = "ReferenceGrant";
    public const string KubeGroup = "gateway.networking.k8s.io";
    public const string KubePluralName = "referencegrants";
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ObjectMeta Metadata { get; set; }

    [JsonPropertyName("spec")] public ReferenceGrantSpec Spec { get; set; }
}

public class ReferenceGrantSpec
{
    [JsonPropertyName("from")] public IList<ReferenceGrantFrom> ReferenceGrantFrom { get; set; }
    [JsonPropertyName("to")] public IList<ReferenceGrantTo> ReferenceGrantTo { get; set; }
}

public class ReferenceGrantTo
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("group")] public string Group { get; set; }
}

public class ReferenceGrantFrom
{
    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("group")] public string Group { get; set; }

    [JsonPropertyName("namespace")] public string Namespace { get; set; }
}
