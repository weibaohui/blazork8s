using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd.Gateway;

public class V1GatewayClassList : IKubernetesObject<V1ListMeta>, IItems<V1GatewayClass>
{
    [JsonPropertyName("items")] public IList<V1GatewayClass> Items { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ListMeta Metadata { get; set; }
}

public class V1GatewayClass : IKubernetesObject<V1ObjectMeta>, ISpec<GatewayClassSpec>
{
    [JsonPropertyName("status")] public GatewayClassStatus Status { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ObjectMeta Metadata { get; set; }

    [JsonPropertyName("spec")] public GatewayClassSpec Spec { get; set; }
}

public class GatewayClassSpec
{
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("controllerName")] public string ControllerName { get; set; }

    [JsonPropertyName("parametersRef")] public ParametersReference ParametersRef { get; set; }
}

public class ParametersReference
{
    [JsonPropertyName("group")] public string Group { get; set; }
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("namespace")] public string Namespace { get; set; }
}

public class GatewayClassStatus
{
    [JsonPropertyName("conditions")] public List<Condition> Conditions { get; set; }

    [JsonPropertyName("supportedFeatures")]
    public List<string> SupportedFeatures { get; set; }
}
