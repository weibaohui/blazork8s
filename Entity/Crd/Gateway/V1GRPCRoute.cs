using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd.Gateway;

public class V1GRPCRouteList : IKubernetesObject<V1ListMeta>, IItems<V1GRPCRoute>
{
    [JsonPropertyName("items")] public IList<V1GRPCRoute> Items { get; set; }
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

    [JsonPropertyName("parentRefs")] public IList<ParentReference> ParentRefs { get; set; }

    [JsonPropertyName("rules")] public IList<GRPCRouteRule> Rules { get; set; }
}

public class GRPCRouteRule
{
    [JsonPropertyName("matches")] public List<GRPCRouteMatch> Matches { get; set; }

    [JsonPropertyName("filters")] public List<GRPCRouteFilter> Filters { get; set; }

    [JsonPropertyName("backendRefs")] public List<GRPCBackendRef> BackendRefs { get; set; }

    [JsonPropertyName("sessionPersistence")]
    public SessionPersistence SessionPersistence { get; set; }
}

public class GRPCBackendRef
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("group")] public string Group { get; set; }
    [JsonPropertyName("namespace")] public string Namespace { get; set; }
    [JsonPropertyName("port")] public int Port { get; set; }
    [JsonPropertyName("weight")] public int Weight { get; set; }
    [JsonPropertyName("filters")] public IList<GRPCRouteFilter> Filters { get; set; }
}

public class GRPCRouteFilter
{
    [JsonPropertyName("type")] public GRPCRouteFilterType Type { get; set; }


    [JsonPropertyName("requestHeaderModifier")]
    public HTTPHeaderFilter RequestHeaderModifier { get; set; }

    [JsonPropertyName("responseHeaderModifier")]
    public HTTPHeaderFilter ResponseHeaderModifier { get; set; }

    [JsonPropertyName("requestMirror")] public HTTPRequestMirrorFilter RequestMirror { get; set; }

    [JsonPropertyName("extensionRef")] public LocalObjectReference ExtensionRef { get; set; }
}

public class GRPCRouteMatch
{
    [JsonPropertyName("method")] public GRPCMethodMatch Method { get; set; }

    [JsonPropertyName("headers")] public IList<GRPCHeaderMatch> Headers { get; set; }
}

public class GRPCHeaderMatch
{
    [JsonPropertyName("value")] public string Value { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("type")] public HeaderMatchType Type { get; set; }
}

public class GRPCMethodMatch
{
    [JsonPropertyName("service")] public string Service { get; set; }

    [JsonPropertyName("method")] public string Method { get; set; }

    [JsonPropertyName("type")] public GRPCMethodMatchType Type { get; set; }
}

public class GRPCRouteStatus
{
    [JsonPropertyName("parents")] public IList<RouteParentStatus> Parents { get; set; }
}
