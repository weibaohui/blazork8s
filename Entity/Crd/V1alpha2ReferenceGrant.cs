using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd;

public class V1alpha2ReferenceGrantList : IKubernetesObject<V1ListMeta>, IItems<V1alpha2ReferenceGrant>
{
    public IList<V1alpha2ReferenceGrant> Items { get; set; }

    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ListMeta Metadata { get; set; }
}

public class V1alpha2ReferenceGrant : IKubernetesObject<V1ObjectMeta>, ISpec<ReferenceGrantSpec>
{
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ObjectMeta Metadata { get; set; }

    [JsonPropertyName("spec")] public ReferenceGrantSpec Spec { get; set; }
}

public class ReferenceGrantSpec
{
    [JsonPropertyName("from")] public IList<string> From { get; set; }

    [JsonPropertyName("to")] public IList<string> To { get; set; }
}
