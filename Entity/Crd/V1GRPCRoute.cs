using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd;

public class V1GRPCRouteList : IKubernetesObject<V1ListMeta>, IItems<V1GRPCRoute>
{
    public IList<V1GRPCRoute> Items { get; set; }

    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ListMeta Metadata { get; set; }
}

public class V1GRPCRoute : IKubernetesObject<V1ObjectMeta>, ISpec<GRPCRouteSpec>
{
    [JsonPropertyName("status")] public GRPCRouteStatus Status { get; set; }

    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ObjectMeta Metadata { get; set; }

    [JsonPropertyName("spec")] public GRPCRouteSpec Spec { get; set; }
}

public class GRPCRouteSpec
{
    [JsonPropertyName("hostnames")] public IList<string> Hostnames { get; set; }

    [JsonPropertyName("parentRefs")] public IList<Parents> ParentRefs { get; set; }

    [JsonPropertyName("rules")] public IList<string> Rules { get; set; }
}

public class GRPCRouteStatus
{
    [JsonPropertyName("parents")] public IList<Parents> Parents { get; set; }
}

public class Parents
{
    [JsonPropertyName("conditions")] public IList<Conditions> Conditions { get; set; }

    [JsonPropertyName("controllerName")] public string ControllerName { get; set; }

    // [JsonPropertyName("parentRef")]
    // public ParentRef ParentRef { get; set; }
}
