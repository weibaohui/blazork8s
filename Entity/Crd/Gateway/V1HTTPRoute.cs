using System.Collections.Generic;
using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

namespace Entity.Crd.Gateway;

public class V1HTTPRouteList : IKubernetesObject<V1ListMeta>, IItems<V1HTTPRoute>
{
    [JsonPropertyName("items")] public IList<V1HTTPRoute> Items { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ListMeta Metadata { get; set; }
}

[KubernetesEntity(Group = KubeGroup, Kind = KubeKind, ApiVersion = KubeApiVersion, PluralName = KubePluralName)]
public class V1HTTPRoute : IKubernetesObject<V1ObjectMeta>, ISpec<HTTPRouteSpec>
{
    public const string KubeApiVersion = "v1";
    public const string KubeKind = "HTTPRoute";
    public const string KubeGroup = "gateway.networking.k8s.io";
    public const string KubePluralName = "httproutes";
    [JsonPropertyName("status")] public HTTPRouteStatus Status { get; set; }
    [JsonPropertyName("apiVersion")] public string ApiVersion { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("metadata")] public V1ObjectMeta Metadata { get; set; }

    [JsonPropertyName("spec")] public HTTPRouteSpec Spec { get; set; }
}

public class HTTPRouteSpec
{
    [JsonPropertyName("hostnames")] public IList<string> Hostnames { get; set; }

    [JsonPropertyName("parentRefs")] public IList<ParentReference> ParentRefs { get; set; }

    [JsonPropertyName("rules")] public IList<HTTPRouteRule> Rules { get; set; }
}

public class HTTPRouteRule
{
    [JsonPropertyName("matches")] public List<HTTPRouteMatch> Matches { get; set; }

    [JsonPropertyName("filters")] public List<HTTPRouteFilter> Filters { get; set; }

    [JsonPropertyName("backendRefs")] public List<HTTPBackendRef> BackendRefs { get; set; }

    [JsonPropertyName("timeouts")] public HTTPRouteTimeouts Timeouts { get; set; }

    [JsonPropertyName("sessionPersistence")]
    public SessionPersistence SessionPersistence { get; set; }
}

public class HTTPRouteTimeouts
{
    [JsonPropertyName("request")] public string Request { get; set; }

    [JsonPropertyName("backendRequest")] public string BackendRequest { get; set; }
}

public class HTTPRouteMatch
{
    [JsonPropertyName("path")] public HTTPPathMatch Path { get; set; }

    [JsonPropertyName("headers")] public IList<HTTPHeaderMatch> Headers { get; set; }

    [JsonPropertyName("queryParams")] public IList<HTTPQueryParamMatch> QueryParams { get; set; }

    [JsonPropertyName("method")] public HTTPMethodType Method { get; set; }
}

public class HTTPPathMatch
{
    [JsonPropertyName("type")] public PathMatchType Type { get; set; }
    [JsonPropertyName("value")] public string Value { get; set; }
}

public class HTTPHeaderMatch
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("type")] public HeaderMatchType Type { get; set; }
    [JsonPropertyName("value")] public string Value { get; set; }
}

public class HTTPQueryParamMatch
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("type")] public QueryParamMatchType Type { get; set; }
    [JsonPropertyName("value")] public string Value { get; set; }
}

public class HTTPRouteFilter
{
    [JsonPropertyName("type")] public HTTPRouteFilterType Type { get; set; }


    [JsonPropertyName("requestHeaderModifier")]
    public HTTPHeaderFilter RequestHeaderModifier { get; set; }

    [JsonPropertyName("responseHeaderModifier")]
    public HTTPHeaderFilter ResponseHeaderModifier { get; set; }

    [JsonPropertyName("requestMirror")] public HTTPRequestMirrorFilter RequestMirror { get; set; }

    [JsonPropertyName("requestRedirect")] public HTTPRequestRedirectFilter RequestRedirect { get; set; }

    [JsonPropertyName("urlRewrite")] public HTTPURLRewriteFilter URLRewrite { get; set; }

    [JsonPropertyName("extensionRef")] public LocalObjectReference ExtensionRef { get; set; }
}

public class HTTPURLRewriteFilter
{
    [JsonPropertyName("hostname")] public string Hostname { get; set; }

    [JsonPropertyName("path")] public HTTPPathModifier Path { get; set; }
}

public class HTTPRequestRedirectFilter
{
    [JsonPropertyName("scheme")] public string Scheme { get; set; }

    [JsonPropertyName("hostname")] public string Hostname { get; set; }

    [JsonPropertyName("path")] public HTTPPathModifier Path { get; set; }

    [JsonPropertyName("port")] public int Port { get; set; }

    [JsonPropertyName("statusCode")] public int StatusCode { get; set; }
}

public class HTTPPathModifier
{
    [JsonPropertyName("type")] public HTTPPathModifierType Type { get; set; }

    [JsonPropertyName("replaceFullPath")] public string ReplaceFullPath { get; set; }

    [JsonPropertyName("replacePrefixMatch")]
    public string ReplacePrefixMatch { get; set; }
}

public class HTTPRequestMirrorFilter
{
    [JsonPropertyName("backendRef")] public BackendObjectReference BackendRef { get; set; }
}

public class HTTPHeaderFilter
{
    [JsonPropertyName("set")] public IList<HTTPHeader> Set { get; set; }

    [JsonPropertyName("add")] public IList<HTTPHeader> Add { get; set; }

    [JsonPropertyName("remove")] public IList<string> Remove { get; set; }
}

public class HTTPHeader
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("value")] public string Value { get; set; }
}

public class HTTPBackendRef : BackendRefWithWeight
{
    [JsonPropertyName("filters")] public List<HTTPRouteFilter> Filters { get; set; }
}

public class HTTPRouteStatus
{
    [JsonPropertyName("parents")] public IList<RouteParentStatus> Parents { get; set; }
}
