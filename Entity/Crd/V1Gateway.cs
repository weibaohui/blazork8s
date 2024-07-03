using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd;

public class V1GatewayList : IKubernetesObject<V1ListMeta>, IItems<V1Gateway>
{
    public IList<V1Gateway> Items { get; set; }

    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ListMeta Metadata { get; set; }
}

public class V1Gateway : IKubernetesObject<V1ObjectMeta>, ISpec<GatewaySpec>
{
    [JsonPropertyName("status")] public GatewayStatus Status { get; set; }

    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ObjectMeta Metadata { get; set; }

    [JsonPropertyName("spec")] public GatewaySpec Spec { get; set; }
}

public class GatewaySpec
{
    [JsonPropertyName("addresses")] public IList<Addresses> Addresses { get; set; }

    [JsonPropertyName("gatewayClassName")] public string GatewayClassName { get; set; }

    [JsonPropertyName("listeners")] public IList<Listeners> Listeners { get; set; }
}

public class GatewayStatus
{
    [JsonPropertyName("addresses")] public IList<Addresses> Addresses { get; set; }

    [JsonPropertyName("conditions")] public IList<Conditions> Conditions { get; set; }

    [JsonPropertyName("listeners")] public IList<Listeners> Listeners { get; set; }
}

public class Addresses
{
    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("value")] public string Value { get; set; }
}

public class Listeners
{
    [JsonPropertyName("attachedRoutes")] public int AttachedRoutes { get; set; }

    [JsonPropertyName("conditions")] public IList<Conditions> Conditions { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("supportedKinds")] public IList<SupportedKinds> SupportedKinds { get; set; }
}

public class SupportedKinds
{
    [JsonPropertyName("group")] public string Group { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }
}
