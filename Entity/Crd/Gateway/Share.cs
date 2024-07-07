using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entity.Crd.Gateway;

public class RouteParentStatus
{
    [JsonPropertyName("parentRef")] public ParentReference ParentRef { get; set; }
    [JsonPropertyName("controllerName")] public string ControllerName { get; set; }
    [JsonPropertyName("conditions")] public IList<Condition> Conditions { get; set; }
}

public class SessionPersistence
{
    [JsonPropertyName("sessionName")] public string SessionName { get; set; }
    [JsonPropertyName("absoluteTimeout")] public string AbsoluteTimeout { get; set; }
    [JsonPropertyName("idleTimeout")] public string IdleTimeout { get; set; }
    [JsonPropertyName("type")] public string Type { get; set; }
    [JsonPropertyName("cookieConfig")] public CookieConfig CookieConfig { get; set; }
}

public class CookieConfig
{
    [JsonPropertyName("lifetimeType")] public string Name { get; set; }
    public CookieLifetimeType LifetimeType { get; set; }
}

public class Condition
{
    [JsonPropertyName("type")] public string Type { get; set; }
    [JsonPropertyName("status")] public string Status { get; set; }
    [JsonPropertyName("reason")] public string Reason { get; set; }
    [JsonPropertyName("message")] public string Message { get; set; }

    [JsonPropertyName("observedGeneration")]
    public int ObservedGeneration { get; set; }

    [JsonPropertyName("lastTransitionTime")]
    public DateTime LastTransitionTime { get; set; }
}

public class LocalObjectReference
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("group")] public string Group { get; set; }
}

public class ParentReference
{
    [JsonPropertyName("group")] public string Group { get; set; }

    [JsonPropertyName("kind")] public string Kind { get; set; }

    [JsonPropertyName("namespace")] public string Namespace { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("sectionName")] public string SectionName { get; set; }

    [JsonPropertyName("port")] public int Port { get; set; }
}

public class BackendObjectReference
{
    [JsonPropertyName("group")] public string Group { get; set; }
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("namespace")] public string Namespace { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("prot")] public int Port { get; set; }
}
