using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entity.Crd;

public class ParentReference
{
    [JsonPropertyName("group")] public string Group { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("namespace")] public string Namespace { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("sectionName")] public string SectionName { get; set; }

    [JsonPropertyName("port")] public int? Port { get; set; }
}

public class CommonRouteSpec
{
    public List<ParentReference> ParentRefs { get; set; }
}

public struct PortNumber
{
    private int value;

    public static implicit operator PortNumber(int value)
    {
        return new PortNumber() { value = value };
    }

    public static implicit operator int(PortNumber portNumber)
    {
        return portNumber.value;
    }
}

public class BackendRef
{
    public BackendObjectReference BackendObjectReference { get; set; }
    public int? Weight { get; set; }
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

public class RouteCondition
{
    public RouteConditionType Type { get; set; }
    public RouteConditionStatus Status { get; set; }
    public RouteConditionReason Reason { get; set; }
    public string Message { get; set; }
    public DateTime LastTransitionTime { get; set; }
}

public class RouteSpec
{
    public CommonRouteSpec CommonSpec { get; set; }
}

public class RouteStatus
{
    [JsonPropertyName("parents")] public IList<RouteParentStatus> Parents { get; set; }
}

public class RouteParentStatus
{
    [JsonPropertyName("parentRef")] public ParentReference ParentRef { get; set; }
    [JsonPropertyName("controllerName")] public string ControllerName { get; set; }
    [JsonPropertyName("conditions")] public IList<Condition> Conditions { get; set; }
}

public class Route
{
    public string ApiVersion { get; set; }
    public string Kind { get; set; }
    public RouteSpec Spec { get; set; }
    public RouteStatus Status { get; set; }
}

public class BackendObjectReference
{
    public string Group { get; set; }
    public string Kind { get; set; }
    public string Name { get; set; }
    public string Namespace { get; set; }
    public int? Port { get; set; }
}

public enum AddressType
{
    IPAddress,
    Hostname,
    NamedAddress
}

public class SessionPersistence
{
    public string SessionName { get; set; }
    public string AbsoluteTimeout { get; set; }
    public string IdleTimeout { get; set; }
    public string Type { get; set; }
    public CookieConfig CookieConfig { get; set; }
}

public enum SessionPersistenceType
{
    Cookie,
    Header
}

public class CookieConfig
{
    public CookieLifetimeType? LifetimeType { get; set; }
}

public enum CookieLifetimeType
{
    Session,
    Permanent
}
