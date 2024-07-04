using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd;

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

    [JsonPropertyName("parametersRef")] public V1TypedLocalObjectReference ParametersRef { get; set; }
}

public enum GatewayClassConditionType
{
    Accepted,
    Scheduled,
    Unsupported,
    SupportedVersion
}

public enum GatewayClassConditionReason
{
    Pending,
    UnsupportedValue,
    SupportedValue,
    UnsupportedVersion,
    SupportedVersion
}

public class GatewayClassStatus
{
    public List<V1Condition> Conditions { get; set; }
    public List<string> SupportedFeatures { get; set; }
}

public class V1Condition
{
    [JsonPropertyName("lastTransitionTime")]
    public DateTime LastTransitionTime { get; set; }

    [JsonPropertyName("message")] public string Message { get; set; }

    [JsonPropertyName("observedGeneration")]
    public int ObservedGeneration { get; set; }

    [JsonPropertyName("reason")] public GatewayClassConditionReason Reason { get; set; }

    [JsonPropertyName("status")] public string Status { get; set; }

    [JsonPropertyName("type")] public GatewayClassConditionType Type { get; set; }
}

public class V1TypedLocalObjectReference
{
    [JsonPropertyName("apiGroup")] public string ApiGroup { get; set; }
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
}

public class Conditions
{
    [JsonPropertyName("lastTransitionTime")]
    public string LastTransitionTime { get; set; }

    [JsonPropertyName("message")] public string Message { get; set; }

    [JsonPropertyName("observedGeneration")]
    public int ObservedGeneration { get; set; }

    [JsonPropertyName("reason")] public string Reason { get; set; }

    [JsonPropertyName("status")] public string Status { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }
}
