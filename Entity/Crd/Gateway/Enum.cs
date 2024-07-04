namespace Entity.Crd.Gateway;

public enum ProtocolType
{
    HTTP,
    HTTPS,
    TLS,
    TCP,
    UDP
}

public enum FromNamespaces
{
    All,
    Selector,
    Same
}

public enum GatewayClassConditionType
{
    Accepted,
    SupportedVersion
}

public enum GatewayClassConditionReason
{
    Accepted,
    InvalidParameters,
    Unsupported,
    Pending,
    SupportedVersion,
    UnsupportedVersion
}

public enum GRPCMethodMatchType
{
    Exact,
    RegularExpression
}

public enum GRPCRouteFilterType
{
    RequestHeaderModifier,
    ResponseHeaderModifier,
    RequestMirror,
    ExtensionRef
}

public enum HTTPRouteFilterType
{
    RequestHeaderModifier,
    ResponseHeaderModifier,
    RequestRedirect,
    URLRewrite,
    RequestMirror,
    ExtensionRef
}

public enum HTTPPathModifierType
{
    ReplaceFullPath,
    ReplacePrefixMatch
}

public enum HTTPMethodType
{
    GET,
    HEAD,
    POST,
    PUT,
    DELETE,
    CONNECT,
    OPTIONS,
    PATCH,
    TRACE
}

public enum PathMatchType
{
    Exact,
    PathPrefix,
    RegularExpression
}

public enum QueryParamMatchType
{
    Exact,
    RegularExpression
}

public enum TLSModeType
{
    Terminate,
    Passthrough
}

public enum SessionPersistenceType
{
    Cookie,
    Header
}

public enum CookieLifetimeType
{
    Session,
    Permanent
}

public enum HeaderMatchType
{
    Exact,
    RegularExpression
}

public enum AddressType
{
    IPAddress,
    Hostname,
    NamedAddress
}

public enum RouteConditionType
{
    Accepted
}

public enum RouteConditionReason
{
    Accepted,
    NotAllowedByListeners,
    NoMatchingListenerHostname,
    NoMatchingParent,
    InvalidKind,
    BackendNotFound,
    UnsupportedProtocol
}

public enum RouteConditionStatus
{
    True,
    False,
    Unknown
}
