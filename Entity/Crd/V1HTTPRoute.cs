using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd;

public class V1HTTPRouteList : IKubernetesObject<V1ListMeta>, IItems<V1HTTPRoute>
{
    public IList<V1HTTPRoute> Items { get; set; }

    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ListMeta Metadata { get; set; }
}

public class V1HTTPRoute : IKubernetesObject<V1ObjectMeta>, ISpec<HTTPRouteSpec>
{
    [JsonPropertyName("status")] public HTTPRouteStatus Status { get; set; }

    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ObjectMeta Metadata { get; set; }

    [JsonPropertyName("spec")] public HTTPRouteSpec Spec { get; set; }
}

public class HTTPRouteSpec
{
    [JsonPropertyName("hostnames")] public IList<string> Hostnames { get; set; }

    [JsonPropertyName("parentRefs")] public IList<HttpParents> ParentRefs { get; set; }

    [JsonPropertyName("rules")] public IList<string> Rules { get; set; }
}

public class HTTPRouteStatus
{
    [JsonPropertyName("parents")] public IList<Parents> Parents { get; set; }
}

public class HttpParents
{
    [JsonPropertyName("conditions")] public IList<Conditions> Conditions { get; set; }

    [JsonPropertyName("controllerName")] public string ControllerName { get; set; }

    [JsonPropertyName("parentRef")] public HttpParents ParentRef { get; set; }
}
