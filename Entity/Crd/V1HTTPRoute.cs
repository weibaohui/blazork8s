using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd;

public class V1HTTPRouteList : IKubernetesObject<V1ListMeta>, IItems<V1HTTPRoute>
{
    [JsonPropertyName("items")] public IList<V1HTTPRoute> Items { get; set; }
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

public class HTTPRoute
{
    public HTTPRouteSpec Spec { get; set; }
    public HTTPRouteStatus Status { get; set; }
}

public class HTTPRouteSpec
{
    [JsonPropertyName("hostnames")] public IList<string> Hostnames { get; set; }

    [JsonPropertyName("parentRefs")] public IList<ParentReference> ParentRefs { get; set; }

    [JsonPropertyName("rules")] public IList<HTTPRouteRule> Rules { get; set; }
}

public class HTTPRouteRule
{
    public List<HTTPRouteMatch> Matches { get; set; }
    public List<HTTPRouteFilter> Filters { get; set; }

    public List<HTTPBackendRef> BackendRefs { get; set; }

    // Timeouts *HTTPRouteTimeouts `json:"timeouts,omitempty"`
    // SessionPersistence *SessionPersistence `json:"sessionPersistence,omitempty"`
    public HTTPRouteTimeouts Timeouts { get; set; }
    public SessionPersistence SessionPersistence { get; set; }
}

public class HTTPRouteTimeouts
{
    public string Request { get; set; }
    public string BackendRequest { get; set; }
}

public class HTTPRouteMatch
{
    public string Path { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public Dictionary<string, string> QueryParams { get; set; }
    public string Method { get; set; }
}

public class HTTPRouteFilter
{
    public HTTPRouteFilterType Type { get; set; }
    public Dictionary<string, string> Parameters { get; set; }
    public HTTPHeaderFilter RequestHeaderModifier { get; set; }
    public HTTPHeaderFilter ResponseHeaderModifier { get; set; }
    public HTTPRequestMirrorFilter RequestMirror { get; set; }
    public HTTPRequestRedirectFilter RequestRedirect { get; set; }
    public HTTPURLRewriteFilter URLRewrite { get; set; }
    public LocalObjectReference ExtensionRef { get; set; }
}

public class LocalObjectReference
{
    public string Name { get; set; }
    public string Kind { get; set; }
    public string Group { get; set; }
}

public class HTTPURLRewriteFilter
{
    public string Hostname { get; set; }
    public HTTPPathModifier Path { get; set; }
}

public class HTTPRequestRedirectFilter
{
    public string Scheme { get; set; }
    public string Hostname { get; set; }
    public HTTPPathModifier Path { get; set; }
    public int Port { get; set; }
    public int StatusCode { get; set; }
}

public class HTTPPathModifier
{
    public HTTPPathModifierType Type { get; set; }
    public string ReplaceFullPath { get; set; }
    public string ReplacePrefixMatch { get; set; }
}

public enum HTTPPathModifierType
{
    ReplaceFullPath,
    ReplacePrefixMatch
}

public class HTTPRequestMirrorFilter
{
    public BackendObjectReference BackendRef { get; set; }
}

public class HTTPHeaderFilter
{
    public IList<HTTPHeader> Set { get; set; }
    public IList<HTTPHeader> Add { get; set; }
    public IList<string> Remove { get; set; }
}

public class HTTPHeader
{
    public string Name { get; set; }
    public string Value { get; set; }
}

public enum HTTPRouteFilterType
{
    RequestHeaderModifier,
    ResponseHeaderModifier,
    RequestRedirect,
    URLRewrite,
    RequestMirror
}

public class HTTPRouteHost
{
    public string Hostname { get; set; }
    public string PathType { get; set; }
    public string Path { get; set; }
    public string PathExact { get; set; }
    public string PathPrefix { get; set; }
    public string PathRegex { get; set; }
}

public class HTTPBackendRef
{
    public string Name { get; set; }
    public string Kind { get; set; }
    public string Group { get; set; }
    public string Namespace { get; set; }
    public int Port { get; set; }
    public int Weight { get; set; }
    public List<HTTPRouteFilter> Filters { get; set; }
}

public class HTTPRouteStatus
{
    public RouteStatus Status { get; set; }
}
